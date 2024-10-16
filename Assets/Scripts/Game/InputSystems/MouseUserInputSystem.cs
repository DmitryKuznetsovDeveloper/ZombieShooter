using System;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.InputSystem;
using Zenject;
namespace Game.Systems.InputSystems
{
    public sealed class MouseUserInputSystem : IInitializable, IDisposable
    {
        private InputAction _mouseDeltaAction;
        private InputAction _mouseScrollAction;
        public float2 MouseInput { get; private set; }
        public float2 MouseScroll { get; private set; }

        void IInitializable.Initialize()
        {
            _mouseDeltaAction = new InputAction("look", binding: "<Mouse>/Delta");
            _mouseDeltaAction.AddCompositeBinding("Dpad")
                .With("Delta","<Gamepad>/LeftStick")
                .With("Up", "<Keyboard>/UpArrow")
                .With("Down", "<Keyboard>/DownArrow")
                .With("Left", "<Keyboard>/LeftArrow")
                .With("Right", "<Keyboard>/RightArrow");

            _mouseDeltaAction.performed += context => { MouseInput = context.ReadValue<Vector2>(); };
            _mouseDeltaAction.started += context => { MouseInput = context.ReadValue<Vector2>(); };
            _mouseDeltaAction.canceled += context => { MouseInput = context.ReadValue<Vector2>(); };
            _mouseDeltaAction.Enable();
            
            _mouseScrollAction =  new InputAction("scroll", binding: "<Mouse>/scroll");
            _mouseScrollAction.performed += context => { MouseScroll = context.ReadValue<Vector2>(); };
            _mouseScrollAction.started += context => { MouseScroll = context.ReadValue<Vector2>(); };
            _mouseScrollAction.canceled += context => { MouseScroll = context.ReadValue<Vector2>(); };
            _mouseScrollAction.Enable();
        }

        void IDisposable.Dispose()
        {
            _mouseDeltaAction?.Dispose();
            _mouseScrollAction?.Dispose();
        }
    }
}