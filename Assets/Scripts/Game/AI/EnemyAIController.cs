using Game.Data;
using Plugins.GameCycle;
using UnityEngine;
using UnityEngine.AI;
namespace Game.AI
{
    public sealed class EnemyAIController : IGameTickable
    {
        private IEnemyStrategy _currentStrategy;
        private readonly NavMeshAgent _navMeshAgent;
        private readonly Animator _animator;
        private readonly Transform _playerTransform;
        private readonly EnemyAIConfig _config;

        private readonly IEnemyStrategy _patrolStrategy;
        private readonly IEnemyStrategy _chaseStrategy;
        private readonly IEnemyStrategy _attackStrategy;

        public EnemyAIController(NavMeshAgent navMeshAgent, Animator animator, CharacterController characterController, EnemyAIConfig config)
        {
            _navMeshAgent = navMeshAgent;
            _animator = animator;
            _playerTransform = characterController.transform;
            _config = config;

            // Инициализация всех стратегий
            _patrolStrategy = new PatrolStrategy(_navMeshAgent, _config);
            _chaseStrategy = new ChaseStrategy(_navMeshAgent, _config);
            _attackStrategy = new AttackStrategy(_config);
            SetStrategy(_patrolStrategy);
        }

        public void Tick(float deltaTime)
        {
            if (IsPlayerInRange(_config.DetectionRange) && !IsPlayerInAttackRange(_config.AttackRange))
            {
                _navMeshAgent.isStopped = false;
                _navMeshAgent.SetDestination(_playerTransform.position);
                _animator.SetFloat(_config.SpeedParameter, _config.ChaseSpeed);
                SetStrategy(_chaseStrategy);
            }
            else if (IsPlayerInAttackRange(_config.AttackRange))
            {
                _navMeshAgent.ResetPath();
                _navMeshAgent.isStopped = true; 
                _animator.SetFloat(_config.SpeedParameter, _config.IdleSpeed);
                SetStrategy(_attackStrategy);
            }
            else
            {
                _navMeshAgent.isStopped = false;
                _animator.SetFloat(_config.SpeedParameter, _config.PatrolSpeed);
                SetStrategy(_patrolStrategy); 
            }
        
            _currentStrategy.Execute(this);
        }

        private void SetStrategy(IEnemyStrategy newStrategy) => _currentStrategy = newStrategy;
    
        private bool IsPlayerInRange(float range) => (GetPlayerPosition() - _animator.transform.position).sqrMagnitude <= range * range;

        private bool IsPlayerInAttackRange(float attackRange) => (GetPlayerPosition() - _animator.transform.position).sqrMagnitude <= attackRange * attackRange;
    
        public Vector3 GetPlayerPosition() => _playerTransform.position;

        public void TriggerAttack() => _animator.SetTrigger(_config.AtackParameter);
    }
}