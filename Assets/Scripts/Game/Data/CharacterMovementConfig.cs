using Sirenix.OdinInspector;
using UnityEngine;
namespace Game.Data
{
    [CreateAssetMenu(fileName = "CharacterMovementConfig", menuName = "Configs/CharacterMovementConfig", order = 0)]
    public class CharacterMovementConfig : ScriptableObject
    {

        [TabGroup("General", "Movement Settings")]
        [Title("Movement Speed")]
        [Range(1f, 20f), Tooltip("The normal walking speed of the character.")]
        public float moveSpeed = 5f;

        [TabGroup("General", "Movement Settings")]
        [Range(1f, 20f), Tooltip("The running speed of the character.")]
        public float runSpeed = 10f;

        [TabGroup("General", "Movement Settings")]
        [MinValue(1f), MaxValue(60f), Tooltip("The time in seconds for stamina to recover after running.")]
        public float staminaRecoveryTime = 5f;
        
        [TabGroup("General", "Movement Settings")]
        [MinValue(1f), MaxValue(60f), Tooltip("Time in seconds, how long it takes to restore endurance after running.")]
        public float staminaRecoveryDelayTime = 5f;

        [TabGroup("General", "Jump and Gravity")]
        [Title("Jump & Gravity")]
        [Range(1f, 20f), Tooltip("The jump force, controlling the height of jumps.")]
        public float jumpForce = 5f;

        [TabGroup("General", "Jump and Gravity")]
        [Tooltip("Gravity applied to the character. Negative value represents downward force.")]
        public float gravity = -9.81f;

        [TabGroup("General", "Mouse Look")]
        [Title("Mouse Sensitivity")]
        [Range(0.1f, 10f), Tooltip("Mouse sensitivity for looking around.")]
        public float mouseSensitivity = 2f;

        [Space]
        [TabGroup("General", "Mouse Look")]
        [Range(0f, 1f), Tooltip("Smoothness of the mouse look rotation to avoid jerky movement.")]
        public float rotationSmoothTime = 0.1f;

        [TabGroup("Info", "Debug Info")]
        [ShowInInspector, ReadOnly, LabelText("Current Move Speed")]
        public float CurrentMoveSpeed => moveSpeed;

        [TabGroup("Info", "Debug Info")]
        [ShowInInspector, ReadOnly, LabelText("Current Run Speed")]
        public float CurrentRunSpeed => runSpeed;
    }
}