using System;
using UnityEngine.InputSystem;
using Zenject;
namespace Game.Systems.InputSystems
{
    public sealed class ReloadUserInputSystem :  IInitializable, ITickable, IDisposable
    {
        private InputAction _reloadAction;
        public bool IsReloadInput{ get; private set; }

        void IInitializable.Initialize()
        {
            _reloadAction = new InputAction("reload", binding: "<Keyboard>/r");
            _reloadAction.Enable();
        }

        void ITickable.Tick() => IsReloadInput = _reloadAction.WasPressedThisFrame();

        void IDisposable.Dispose() => _reloadAction?.Dispose();
    }
}