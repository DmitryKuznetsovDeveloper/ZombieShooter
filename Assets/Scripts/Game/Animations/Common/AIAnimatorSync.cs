using System;
using Game.Data;
using UnityEngine;
using UnityEngine.AI;
using Zenject;
namespace Game.Animations.Common
{
    public class SyncOnAnimatorMove : MonoBehaviour
    {
        private NavMeshAgent _agent;
        private Animator _animator;
        private EnemyAIConfig _enemyAIConfig;

        [Inject]
        public void Construct(NavMeshAgent agent,Animator animator,EnemyAIConfig enemyAIConfig)
        {
            _agent = agent;
            _animator = animator;
            _enemyAIConfig = enemyAIConfig;
        }

        private void OnAnimatorMove()
        {
            // Применяем root motion для перемещения агента
            _agent.Move(_animator.deltaPosition);

            // Синхронизируем вращение
            if (_agent.desiredVelocity.sqrMagnitude > 0.1f)
            {
                Quaternion targetRotation = Quaternion.LookRotation(_agent.desiredVelocity.normalized);
                _agent.transform.rotation = Quaternion.Slerp(_agent.transform.rotation, targetRotation, _enemyAIConfig.RotationSpeed * Time.deltaTime);
            }
        }
    }
}