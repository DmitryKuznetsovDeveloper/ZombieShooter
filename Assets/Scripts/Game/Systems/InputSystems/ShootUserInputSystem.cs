using System;
using UnityEngine.InputSystem;
using Zenject;
namespace Game.Systems.InputSystems
{
    public sealed class ShootUserInputSystem : IInitializable,ITickable, IDisposable
    {
        private InputAction _shootActionLeft;
        private InputAction _aimAction;
        public bool IsShootInput { get; private set; }
        public bool IsAimInput{ get; private set; }

        void IInitializable.Initialize()
        {
            _shootActionLeft = new InputAction("shoot", binding: "<Mouse>/LeftButton");
            _shootActionLeft.Enable();
            
            _aimAction = new InputAction("aim", binding: "<Mouse>/RightButton");
            _aimAction.Enable();
        }

        void ITickable.Tick()
        {
            IsShootInput = _shootActionLeft.IsPressed();
            IsAimInput = _aimAction.IsPressed();
        }

        void IDisposable.Dispose()
        {
            _shootActionLeft?.Dispose();
            _aimAction?.Dispose();
        }
    }
}