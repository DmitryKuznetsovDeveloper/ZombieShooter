using Game.Data;
using UnityEngine;
using UnityEngine.AI;
namespace Game.AI
{
    public sealed class PatrolStrategy : IEnemyStrategy
    {
        private readonly EnemyAIConfig _config;
        private Vector3 _targetPatrolPoint;
        private float _waitTime;
        private bool _isWaiting;

        public PatrolStrategy(EnemyAIConfig config)
        {
            _config = config;
            _waitTime = _config.WaitTimeBeforeNextPoint;
        }

        public void Execute(EnemyAIController context)
        {
            if (HasReachedDestination(_config.ArrivalDistance, context.NavMeshAgent))
            {
                if (!_isWaiting)
                {
                    _isWaiting = true;
                    _waitTime = _config.WaitTimeBeforeNextPoint;
                    context.UpdateAnimatorSpeed(_config.IdleSpeed, _config.SpeedChangeSpeed);
                }

                _waitTime -= Time.deltaTime;

                if (_waitTime <= 0)
                {
                    _isWaiting = false;
                    _targetPatrolPoint = GetNextValidPatrolPoint(context.NavMeshAgent);
                    context.NavMeshAgent.SetDestination(_targetPatrolPoint);
                }
            }
            else
            {
                if (!_isWaiting)
                    context.UpdateAnimatorSpeed(_config.PatrolSpeed, _config.SpeedChangeSpeed);
            }
        }

        private Vector3 GetNextValidPatrolPoint(NavMeshAgent navMeshAgent)
        {
            var result = navMeshAgent.transform.position;
            int attempts = 0;
            do
            {
                Vector3 randomDirection = Random.insideUnitSphere * _config.PatrolRadius;
                randomDirection += navMeshAgent.transform.position;

                if (NavMesh.SamplePosition(randomDirection, out var hit, _config.PatrolRadius, NavMesh.AllAreas))
                {
                    result = hit.position;
                    break;
                }

                attempts++;
            } while (attempts < 10);

            return result;
        }

        private bool HasReachedDestination(float arrivalDistance, NavMeshAgent agent) => 
            !agent.pathPending && agent.remainingDistance <= arrivalDistance;
    }
}