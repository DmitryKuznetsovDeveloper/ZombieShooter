using System.Collections.Generic;
using Plugins.Zenject.Source.Runtime.Kernels;
using UnityEngine;
using Zenject;
namespace Plugins.GameCycle
{
    public sealed class GameKernel : MonoKernel,
        IGameStartListener, 
        IGamePauseListener, 
        IGameResumeListener,
        IGameFinishListener
    {
        [Inject]
        private GameManager _gameManager;
        
        [InjectLocal]
        private List<IGameListener> _listeners = new();

        [Inject(Optional = true, Source = InjectSources.Local)]
        private List<IGameTickable> _tickables = new();

        [Inject(Optional = true, Source = InjectSources.Local)]
        private List<IGameFixedTickable> _fixedTickables = new();

        [Inject(Optional = true, Source = InjectSources.Local)]
        private List<IGameLateTickable> _lateTickables = new();

        public override void Start()
        {
            base.Start();
            this._gameManager.AddListener(this);
        }
        
        public override  void Update()
        {
            base.Update();

            if (this._gameManager.State == GameState.PLAY)
            {
                float deltaTime = Time.deltaTime;
                foreach (var tickable in this._tickables)
                {
                    tickable.Tick(deltaTime);
                }
            }
        }

        public override void FixedUpdate()
        {
            base.FixedUpdate();

            if (this._gameManager.State == GameState.PLAY)
            {
                float deltaTime = Time.fixedDeltaTime;
                foreach (var tickable in this._fixedTickables)
                {
                    tickable.FixedTick(deltaTime);
                }
            }
        }

        public override void LateUpdate()
        {
            base.LateUpdate();

            if (this._gameManager.State == GameState.PLAY)
            {
                float deltaTime = Time.deltaTime;
                foreach (var tickable in this._lateTickables)
                {
                    tickable.LateTick(deltaTime);
                }
            }
        }

        public override void OnDestroy()
        {
            base.OnDestroy();
            this._gameManager.RemoveListener(this);
        }

        void IGameStartListener.OnStartGame()
        {
            foreach (var it in this._listeners)
            {
                if (it is IGameStartListener listener)
                {
                    listener.OnStartGame();
                }
            }
        }

        void IGamePauseListener.OnPauseGame()
        {
            foreach (var it in this._listeners)
            {
                if (it is IGamePauseListener listener)
                {
                    listener.OnPauseGame();
                }
            }
        }

        void IGameResumeListener.OnResumeGame()
        {
            foreach (var it in this._listeners)
            {
                if (it is IGameResumeListener listener)
                {
                    listener.OnResumeGame();
                }
            }
        }

        void IGameFinishListener.OnFinishGame()
        {
            foreach (var it in this._listeners)
            {
                if (it is IGameFinishListener listener)
                {
                    listener.OnFinishGame();
                }
            }
        }
    }
}