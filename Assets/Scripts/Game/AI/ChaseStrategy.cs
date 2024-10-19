using Game.Data;
using UnityEngine;
using UnityEngine.AI;
namespace Game.AI
{
    public sealed class ChaseStrategy : IEnemyStrategy
    {
        private readonly NavMeshAgent _navMeshAgent;
        private readonly EnemyAIConfig _config;

        public ChaseStrategy(NavMeshAgent navMeshAgent, EnemyAIConfig config)
        {
            _navMeshAgent = navMeshAgent;
            _config = config;
        }

        public void Execute(EnemyAIController context)
        {
            Vector3 playerPosition = context.GetPlayerPosition();
            MoveToPoint(playerPosition, _config.ChaseSpeed);
        }

        private void MoveToPoint(Vector3 destination, float speed)
        {
            _navMeshAgent.speed = speed;
            _navMeshAgent.SetDestination(destination);
        }
    }
}