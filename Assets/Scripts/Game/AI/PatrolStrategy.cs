using Game.Data;
using UnityEngine;
using UnityEngine.AI;
namespace Game.AI
{
    public sealed class PatrolStrategy : IEnemyStrategy
    {
        private readonly NavMeshAgent _navMeshAgent;
        private readonly EnemyAIConfig _config;
        private Vector3 _targetPatrolPoint;
        private float _waitTime;

        public PatrolStrategy(NavMeshAgent navMeshAgent, EnemyAIConfig config)
        {
            _navMeshAgent = navMeshAgent;
            _config = config;
            _targetPatrolPoint = GetNextPatrolPoint();
            _waitTime = _config.WaitTimeBeforeNextPoint;
        }

        public void Execute(EnemyAIController context)
        {
            if (HasReachedDestination(_config.ArrivalDistance))
            {
                _waitTime -= Time.deltaTime;
                if (_waitTime <= 0)
                {
                    _targetPatrolPoint = GetNextPatrolPoint();
                    _waitTime = _config.WaitTimeBeforeNextPoint;
                }
            }
            else
            {
                MoveToPoint(_targetPatrolPoint, _config.PatrolSpeed);
            }
        }

        private Vector3 GetNextPatrolPoint()
        {
            Vector3 randomDirection = Random.insideUnitSphere * _config.PatrolRadius;
            randomDirection += _navMeshAgent.transform.position;

            NavMesh.SamplePosition(randomDirection, out var hit, _config.PatrolRadius, 1);
            return hit.position;
        }

        private void MoveToPoint(Vector3 destination, float speed)
        {
            _navMeshAgent.speed = speed;
            _navMeshAgent.SetDestination(destination);
        }

        private bool HasReachedDestination(float arrivalDistance) => 
            !_navMeshAgent.pathPending && _navMeshAgent.remainingDistance <= arrivalDistance;
    }
}