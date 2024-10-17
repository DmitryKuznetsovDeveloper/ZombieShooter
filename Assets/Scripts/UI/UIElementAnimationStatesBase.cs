﻿using System;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

namespace UI
{
    public abstract class UIElementAnimationStatesBase<TState, TSettings,TConfig> : MonoBehaviour 
        where TState : Enum
        where TConfig : ScriptableObject
        where TSettings : class
    {
        [SerializeField] protected TConfig _config;
        protected Dictionary<TState, Sequence> AnimationSequences;
        private TState _currentState;
        private bool _isFirstRun = true;

        public TState CurrentState => _currentState;
        
        protected abstract void InitializeSequences();
        
        protected abstract Sequence CreateSequence(TSettings stateConfig);
        
        protected abstract TState GetDefaultState();
    
        protected virtual void Awake()
        {
            _currentState = GetDefaultState();
            InitializeSequences();
        }

        private void OnDisable()
        {
            foreach (var sequences in AnimationSequences.Values)
                sequences.OnComplete(()=> sequences?.Pause())?.Rewind();
        }
        
        private void OnDestroy()
        {
            foreach (var sequences in AnimationSequences.Values)
                sequences?.Kill();
        }

        public virtual void PlayForwardAnimation(TState state)
        {
            if (_currentState.Equals(state) && !_isFirstRun) return;

            // Остановить текущую анимацию, если она есть
            if (AnimationSequences.TryGetValue(_currentState, out var currentSequence))
                currentSequence?.Pause();

            // Запустить новую анимацию
            if (AnimationSequences.TryGetValue(state, out var newSequence))
            {
                newSequence?.Restart();
                _currentState = state;
                _isFirstRun = false;
            }
        }
    }
}