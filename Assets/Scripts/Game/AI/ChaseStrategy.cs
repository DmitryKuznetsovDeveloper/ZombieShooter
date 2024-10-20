using Game.Data;
using UnityEngine;
using UnityEngine.AI;

namespace Game.AI
{
    public sealed class ChaseStrategy : IEnemyStrategy
    {
        private readonly EnemyAIConfig _config;
        private Vector3 _lastPlayerPosition;

        public ChaseStrategy(EnemyAIConfig config)
        {
            _config = config;
        }

        public void Execute(EnemyAIController context)
        {
            context.NavMeshAgent.isStopped = false;
            Vector3 playerPosition = context.GetPlayerPosition();
            if (HasReachedDestination(context.NavMeshAgent))
            {
                context.UpdateAnimatorSpeed(0, _config.SpeedChangeSpeed);
                context.NavMeshAgent.isStopped = true;
                return;
            }
            
            if (Vector3.Distance(playerPosition, _lastPlayerPosition) > 1f)
            {
                _lastPlayerPosition = playerPosition;
                context.NavMeshAgent.SetDestination(playerPosition);
            }
            
            context.UpdateAnimatorSpeed(_config.ChaseSpeed, _config.SpeedChangeSpeed);
        }
        
        private bool HasReachedDestination(NavMeshAgent agent) =>
            !agent.pathPending && agent.remainingDistance <= _config.AttackRange;
    }
}