using Game.Data;
using UnityEngine;

namespace Game.AI
{
    public sealed class ChaseStrategy : IEnemyStrategy
    {
        private readonly EnemyAIConfig _config;
        private Vector3 _lastPlayerPosition;
        private const float PositionUpdateThreshold = 0.2f; 

        public ChaseStrategy(EnemyAIConfig config) => _config = config;

        public void Execute(EnemyAIController context)
        {
            var playerPosition = context.GetPlayerPosition();
            float distanceToPlayer = Vector3.Distance(context.Animator.transform.position, playerPosition);
            
            if (distanceToPlayer > _config.DetectionRange)
            {
                context.NavMeshAgent.isStopped = true;
                context.UpdateAnimatorSpeed(_config.IdleSpeed, _config.SpeedChangeSpeed);
                return;
            }
            
            if (distanceToPlayer <= _config.AttackRange)
            {
                context.NavMeshAgent.isStopped = true;
                context.UpdateAnimatorSpeed(_config.IdleSpeed, 10000);
                return;
            }
            
            context.NavMeshAgent.isStopped = false;
            
            if (Vector3.Distance(playerPosition, _lastPlayerPosition) > PositionUpdateThreshold)
            {
                _lastPlayerPosition = playerPosition;
                context.NavMeshAgent.SetDestination(playerPosition);
            }
            
            context.UpdateAnimatorSpeed(_config.ChaseSpeed, _config.SpeedChangeSpeed);
        }
    }
}
