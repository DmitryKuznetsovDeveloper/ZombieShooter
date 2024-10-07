using System;
using UnityEngine.InputSystem;
using Zenject;
namespace Game.Systems.InputSystems
{
    public sealed class JumpUserInputSystem : IInitializable, ITickable, IDisposable
    {
        private InputAction _jumpAction;
        public bool IsJumpInput { get;private set; }

        void IInitializable.Initialize()
        {
            _jumpAction = new InputAction("move", binding: "<Keyboard>/space");
            _jumpAction.Enable();
        }

        void ITickable.Tick() => IsJumpInput = _jumpAction.IsPressed();

        void IDisposable.Dispose() => _jumpAction?.Dispose();
    }
}