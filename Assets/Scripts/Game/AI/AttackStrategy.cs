using Game.Data;
using UnityEngine;
namespace Game.AI
{
    public sealed class AttackStrategy : IEnemyStrategy
    {
        private readonly EnemyAIConfig _config;
        private float _attackCooldown;

        public AttackStrategy(EnemyAIConfig config) => _config = config;

        public void Execute(EnemyAIController context)
        {
            if (_attackCooldown <= 0)
            {
                context.TriggerAttack();
                _attackCooldown = _config.AttackCooldown;
            }
            else
                _attackCooldown -= Time.deltaTime;
        }
    }
}