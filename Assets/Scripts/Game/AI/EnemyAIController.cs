using Game.Data;
using Plugins.GameCycle;
using UnityEngine;
using UnityEngine.AI;
namespace Game.AI
{
    public sealed class EnemyAIController : IGameTickable
    {
        public NavMeshAgent NavMeshAgent => _navMeshAgent;
        private IEnemyStrategy _currentStrategy;
        private readonly NavMeshAgent _navMeshAgent;
        private readonly Animator _animator;
        private readonly Transform _playerTransform;
        private readonly EnemyAIConfig _config;

        private readonly IEnemyStrategy _patrolStrategy;
        private readonly IEnemyStrategy _chaseStrategy;
        private readonly IEnemyStrategy _attackStrategy;
        private float _currentSpeed;

        public EnemyAIController(NavMeshAgent navMeshAgent, Animator animator, CharacterController characterController, EnemyAIConfig config)
        {
            _navMeshAgent = navMeshAgent;
            _animator = animator;
            _playerTransform = characterController.transform;
            _config = config;

            // Инициализация всех стратегий
            _patrolStrategy = new PatrolStrategy(_config);
            _chaseStrategy = new ChaseStrategy(_config);
            _attackStrategy = new AttackStrategy(_config);
            SetStrategy(_patrolStrategy);
        }

        public void Tick(float deltaTime)
        {
            if (IsPlayerInRange(_config.DetectionRange) && !IsPlayerInAttackRange(_config.AttackRange))
                SetStrategy(_chaseStrategy);

            else if (IsPlayerInAttackRange(_config.AttackRange))
                SetStrategy(_attackStrategy);
            else
            {
                SetStrategy(_patrolStrategy); 
            }
        
            _currentStrategy.Execute(this);
        }

        private void SetStrategy(IEnemyStrategy newStrategy) => _currentStrategy = newStrategy;
    
        private bool IsPlayerInRange(float range) => (GetPlayerPosition() - _animator.transform.position).sqrMagnitude <= range * range;

        private bool IsPlayerInAttackRange(float attackRange) => (GetPlayerPosition() - _animator.transform.position).sqrMagnitude <= attackRange * attackRange;
    
        public Vector3 GetPlayerPosition() => _playerTransform.position;

        public void AnimationAttack(int value) => _animator.SetInteger(_config.AtackParameter, value);

        public void UpdateAnimatorSpeed(float targetSpeed, float speedChangeSpeed = 0)
        {
            _currentSpeed = Mathf.MoveTowards(_currentSpeed, targetSpeed, speedChangeSpeed * Time.deltaTime);
            _animator.SetFloat(_config.SpeedParameter, _currentSpeed);
        }
        public bool HasEndedAnimationAttack()
        {
            AnimatorStateInfo stateInfo = _animator.GetCurrentAnimatorStateInfo(1);
            return stateInfo.IsName("Attack") || stateInfo.IsName("Attack_2") || stateInfo.IsName("Attack_3") && stateInfo.normalizedTime >= 1.0f;
        }
    }
}