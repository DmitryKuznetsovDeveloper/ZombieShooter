using Game.Animations.Common;
using Game.Data;
using Plugins.GameCycle;
using UnityEngine;
using UnityEngine.AI;
namespace Game.AI
{
    public sealed class EnemyAIController : IGameTickable
    {
        public NavMeshAgent NavMeshAgent => _navMeshAgent;
        public Animator Animator => _animator;
        public TokenBase CancellationToken => _cancellationToken;
        private IEnemyStrategy _currentStrategy;
        private readonly NavMeshAgent _navMeshAgent;
        private readonly Animator _animator;
        private readonly Transform _playerTransform;
        private readonly EnemyAIConfig _config;
        private readonly TokenBase _cancellationToken;

        private readonly IEnemyStrategy _patrolStrategy;
        private readonly IEnemyStrategy _chaseStrategy;
        private readonly IEnemyStrategy _attackStrategy;

        public EnemyAIController(
            NavMeshAgent navMeshAgent, 
            Animator animator, 
            CharacterController characterController, 
            EnemyAIConfig config, TokenBase token)
        {
            _navMeshAgent = navMeshAgent;
            _animator = animator;
            _playerTransform = characterController.transform;
            _config = config;
            _cancellationToken = token;

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
                SetStrategy(_patrolStrategy);

            _currentStrategy.Execute(this);
        }


        private void SetStrategy(IEnemyStrategy newStrategy) => _currentStrategy = newStrategy;

        private bool IsPlayerInRange(float range) => (GetPlayerPosition() - _animator.transform.position).sqrMagnitude <= range * range;

        private bool IsPlayerInAttackRange(float attackRange) => (GetPlayerPosition() - _animator.transform.position).sqrMagnitude <= attackRange * attackRange;

        public Vector3 GetPlayerPosition() => _playerTransform.position;

        public void UpdateAnimatorSpeed(float targetSpeed, float speedChangeSpeed = 0)
        {
            float currentSpeed = _animator.GetFloat(_config.SpeedParameter);
            float newSpeed = Mathf.MoveTowards(currentSpeed, targetSpeed, speedChangeSpeed * Time.deltaTime);
            _animator.SetFloat(_config.SpeedParameter, newSpeed);
        }
    }
}
