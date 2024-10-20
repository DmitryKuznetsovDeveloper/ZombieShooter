using Game.Data;
using UnityEngine;

namespace Game.AI
{
    public sealed class AttackStrategy : IEnemyStrategy
    {
        private readonly EnemyAIConfig _config;
        private float _attackCooldown;
        private int _previousAttackValue;

        public AttackStrategy(EnemyAIConfig config) => _config = config;

        public void Execute(EnemyAIController context)
        {
            //TODO: доделать разную атаку, проблема с сбросом и вызовом анимаций
            context.NavMeshAgent.ResetPath();
            context.NavMeshAgent.isStopped = true;
            context.UpdateAnimatorSpeed(_config.IdleSpeed, 1000);
            if (_attackCooldown <= 0)
            {
                context.AnimationAttack(GetNextAttackValue());
                _attackCooldown = _config.AttackCooldown;
            }
            else
                _attackCooldown -= Time.deltaTime;
        }

        private int GetNextAttackValue()
        {
            int newAttackValue;
            do
            {
                newAttackValue = Random.Range(1, 4);
            } while (newAttackValue == _previousAttackValue);
            _previousAttackValue = newAttackValue;
            return newAttackValue;
        }
    }
}