using System;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.InputSystem;
using Zenject;
namespace Game.Systems.InputSystems
{
    public sealed class MoveUserInputSystem : IInitializable, IDisposable
    {
        private InputAction _moveActon;
        public float2 MoveInput { get; private set; }

        void IInitializable.Initialize()
        {
            _moveActon = new InputAction("move", binding: "<Gamepad>/LeftStick");
            _moveActon.AddCompositeBinding("Dpad")
                .With("Up", "<Keyboard>/w")
                .With("Down", "<Keyboard>/s")
                .With("Left", "<Keyboard>/a")
                .With("Right", "<Keyboard>/d");

            _moveActon.started += context => { MoveInput = context.ReadValue<Vector2>(); };
            _moveActon.performed += context => { MoveInput = context.ReadValue<Vector2>(); };
            _moveActon.canceled += context => { MoveInput = context.ReadValue<Vector2>(); };
            _moveActon.Enable();
        }

        void IDisposable.Dispose() => _moveActon?.Dispose();
    }
}