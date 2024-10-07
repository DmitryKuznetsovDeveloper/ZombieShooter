using System;
using UnityEngine.InputSystem;
using Zenject;
namespace Game.Systems.InputSystems
{
    public sealed class RunUserInputSystem : IInitializable, ITickable, IDisposable
    {
        private InputAction _runAction;
        public bool IsRunInput { get; private set; }

        void IInitializable.Initialize()
        {
            _runAction = new InputAction("run", binding: "<Keyboard>/Shift");
            _runAction.Enable();
        }

        void ITickable.Tick() => IsRunInput = _runAction.IsPressed();

        void IDisposable.Dispose() => _runAction?.Dispose();
    }
}