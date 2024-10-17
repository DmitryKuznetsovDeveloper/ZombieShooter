using Game.Components;
using Plugins.GameCycle;
using Zenject;

namespace UI.MediatorUI
{
    public sealed class HealthBarMediator : IInitializable, IGameFinishListener
    {
        private readonly CharacterInstaller _characterInstaller;
        private readonly HealthBarView _healthBarView;

        private HealthComponent _characterHealthComponent;
        private enum HealthState { Normal, Warning, Danger }
        private HealthState _previousHealthState;

        public HealthBarMediator(CharacterInstaller characterInstaller, HealthBarView healthBarView)
        {
            _characterInstaller = characterInstaller;
            _healthBarView = healthBarView;
      
        }
        void IInitializable.Initialize()
        {
            _characterHealthComponent = _characterInstaller.GetComponent<HealthComponent>();
            _characterHealthComponent.OnChangeHealth += OnHealthChangeAnimationState;
            _characterHealthComponent.OnChangeHealth +=  _healthBarView.SetHealthPointLabel;
            _characterHealthComponent.OnChangeHealth +=  _healthBarView.SetHealthBarFill;
        }
        
        private void OnHealthChangeAnimationState(int currentHealthPoints)
        {
            // Определение текущего состояния здоровья
            HealthState currentHealthState = currentHealthPoints switch
            {
                >= 70 => HealthState.Normal,
                >= 40 => HealthState.Warning,
                < 40 => HealthState.Danger,
            };
            switch (currentHealthState)
            {
                case HealthState.Normal:
                    _healthBarView.PlayForwardAnimation(HealthBarView.HealthBarAnimationState.Normal);
                    break;
                case HealthState.Warning:
                    _healthBarView.PlayForwardAnimation(HealthBarView.HealthBarAnimationState.Warning);
                    break;
                case HealthState.Danger:
                    _healthBarView.PlayForwardAnimation(HealthBarView.HealthBarAnimationState.Danger);
                    break;
            }
        }
        public void OnFinishGame()
        {
            _characterHealthComponent.OnChangeHealth -= OnHealthChangeAnimationState;
            _characterHealthComponent.OnChangeHealth -=  _healthBarView.SetHealthPointLabel;
            _characterHealthComponent.OnChangeHealth -=  _healthBarView.SetHealthBarFill;
        }
    }
}