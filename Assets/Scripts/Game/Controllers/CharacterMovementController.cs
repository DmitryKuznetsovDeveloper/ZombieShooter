using Game.Controllers;
using Game.Systems.InputSystems;
using Plugins.GameCycle;

namespace Controllers
{
    public sealed class CharacterMovementController : IGameTickable
    {
        private readonly MouseUserInputSystem _mouseUserInputSystem;
        private readonly MoveUserInputSystem _moveUserInputSystem;
        private readonly RunUserInputSystem _runUserInputSystem;
        private readonly JumpUserInputSystem _jumpUserInputSystem;
        private readonly CharacterMovement _characterMovement;
        
        public CharacterMovementController(
            MouseUserInputSystem mouseUserInputSystem, 
            MoveUserInputSystem moveUserInputSystem, 
            RunUserInputSystem runUserInputSystem, 
            JumpUserInputSystem jumpUserInputSystem, 
            CharacterMovement characterMovement)
        {
            _mouseUserInputSystem = mouseUserInputSystem;
            _moveUserInputSystem = moveUserInputSystem;
            _runUserInputSystem = runUserInputSystem;
            _jumpUserInputSystem = jumpUserInputSystem;
            _characterMovement = characterMovement;
        }

        public void Tick(float deltaTime)
        {
            _characterMovement.MoveCharacterOrJump(
                _moveUserInputSystem.MoveInput,
                _runUserInputSystem.IsRunInput,
                _jumpUserInputSystem.IsJumpInput);
            _characterMovement.LookAround(_mouseUserInputSystem.MouseInput);
        }
    }
}