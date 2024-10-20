using UnityEngine;
namespace Game.Data
{
    [CreateAssetMenu(fileName = "EnemyAIConfig", menuName = "Configs/EnemyAIConfig", order = 0)]
    public class EnemyAIConfig : ScriptableObject
    {
        [Header("Movement Settings")]
        public float IdleSpeed;
        public float PatrolSpeed = 0.5f;
        public float ChaseSpeed = 1f;
        public float SpeedChangeSpeed = 1;
        public float ArrivalDistance = 0.5f;
        public float PatrolRadius = 10f;
        public float WaitTimeBeforeNextPoint = 2;
        
        [Header("Attack Settings")]
        public float DetectionRange = 10f;
        public float AttackRange = 2f;
        public float AttackCooldown = 1f;

        [Header("Animator Parameters")]
        public string SpeedParameter = "Speed";
        public string AtackParameter = "Attack";
    }
}

