using UnityEngine;
namespace Game.Data
{
    [CreateAssetMenu(fileName = "EnemyAIConfig", menuName = "Configs/EnemyAIConfig", order = 0)]
    public class EnemyAIConfig : ScriptableObject
    {
        public float MovementSpeed;
        public float StoppingDistance;
        public float RotationSpeed;
        public string AnimationMovementParameter = "Run";
    }
}