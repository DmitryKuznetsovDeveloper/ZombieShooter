using System;
using UnityEngine.InputSystem;
using Zenject;
namespace Game.Systems.InputSystems
{
    public sealed class PauseUserInputSystem : IInitializable, ITickable, IDisposable
    {
        private InputAction _pauseAction;
        public bool IsPauseInput{ get; private set; }
        
        void IInitializable.Initialize()
        {
            _pauseAction = new InputAction("pause", binding: "<Keyboard>/Escape");
            _pauseAction.Enable();
        }

        void ITickable.Tick() => IsPauseInput = _pauseAction.WasPressedThisFrame();
        void IDisposable.Dispose() => _pauseAction?.Dispose();
    }
}