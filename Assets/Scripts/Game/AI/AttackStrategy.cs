using Cysharp.Threading.Tasks;
using Game.Animations.Common;
using Game.Data;
using UnityEngine;

namespace Game.AI
{
    public sealed class AttackStrategy : IEnemyStrategy
    {
        private readonly EnemyAIConfig _config;
        private float _attackCooldown;
        private int _previousAttackValue;
        private bool _isAttacking; // Флаг, контролирующий активность атаки

        public AttackStrategy(EnemyAIConfig config)
        {
            _config = config;
            _isAttacking = false; // Атака не выполняется по умолчанию
        }

        public void Execute(EnemyAIController context)
        {
            // Проверяем, идет ли атака или кулдаун
            if (_isAttacking || _attackCooldown > 0)
            {
                if (_attackCooldown > 0)
                    _attackCooldown -= Time.deltaTime;
                
                // Если атака активна, проверяем состояние анимации
                if (_isAttacking)
                    CheckAttackAnimation(context);
                
                return; // Не запускаем новую атаку, пока предыдущая не завершена
            }
            
            StartAttack(context);
        }

        private void StartAttack(EnemyAIController context)
        {
            _isAttacking = true;
            context.UpdateAnimatorSpeed(_config.IdleSpeed, 10000);
            int attackValue = GetNextAttackValue();
            
            // Определяем, какую атаку запустить
            switch (attackValue)
            {
                case 1:
                    context.Animator.SetTrigger(_config.AttackNameOne);
                    break;
                case 2:
                    context.Animator.SetTrigger(_config.AttackNameTwo);
                    break;
                case 3:
                    context.Animator.SetTrigger(_config.AAttackNameThree);
                    break;
            }
        }

        private async void CheckAttackAnimation(EnemyAIController context)
        {
            await WaitAttackAnimationEnd(context);
            EndAttack();
        }
        private async UniTask WaitAttackAnimationEnd(EnemyAIController context)
        {
            int layer = context.Animator.GetLayerIndex(_config.AttackLayer);
            if (layer == -1) return;

            // Ожидаем окончания анимации на указанном слое
            await UniTask.WaitUntil(() => !context.Animator.IsAnimationPlaying( layer, _config.AttackNameOne), cancellationToken: context.CancellationToken.Token);
            await UniTask.WaitUntil(() => !context.Animator.IsAnimationPlaying( layer,  _config.AttackNameTwo), cancellationToken: context.CancellationToken.Token);
            await UniTask.WaitUntil(() => !context.Animator.IsAnimationPlaying( layer,  _config.AAttackNameThree), cancellationToken: context.CancellationToken.Token);
        }

        private void EndAttack()
        {
            _isAttacking = false; // Сбрасываем флаг после завершения атаки
            _attackCooldown = _config.AttackCooldown; // Начинаем отсчет кулдауна
        }
        

        // Получаем случайное значение для следующей атаки
        private int GetNextAttackValue()
        {
            int newAttackValue;
            do
            {
                newAttackValue = Random.Range(1, 4); // Значения от 1 до 3
            } while (newAttackValue == _previousAttackValue); // Избегаем повторов
            _previousAttackValue = newAttackValue;
            return newAttackValue;
        }
    }
}
