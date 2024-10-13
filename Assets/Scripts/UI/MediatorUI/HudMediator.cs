using System;
using Game.Components;
using Plugins.GameCycle;
using Plugins.Zenject.Source.Main;
using Zenject;

namespace UI.MediatorUI
{
    public sealed class HudMediator : IInitializable, IGameTickable
    {
        private readonly DiContainer _diContainer;
        private readonly HealthBarView _healthBarView;
        private readonly WeaponInfoView _weaponInfoView;

        private HealthComponent _characterHealthComponent;
        private enum HealthState { Normal, Warning, Danger }
        private HealthState _previousHealthState;

        public HudMediator(DiContainer diContainer, HealthBarView healthBarView, WeaponInfoView weaponInfoView)
        {
            _diContainer = diContainer;
            _healthBarView = healthBarView;
            _weaponInfoView = weaponInfoView;
        }

        public void Initialize()
        {
            _characterHealthComponent = _diContainer.Resolve<CharacterInstaller>().GetComponent<HealthComponent>();
        }

        public void Tick(float deltaTime)
        {
            UpdateHealthBar();
        }

        private void UpdateHealthBar()
        {
            int currentHealth = _characterHealthComponent.CurrentHealthPoints;
            int maxHealth = _characterHealthComponent.MaxHitPoints;

            _healthBarView.SetHealthPointLabel(currentHealth);
            _healthBarView.SetHealthBarFill(currentHealth, maxHealth);
            UpdateHealthState(currentHealth);
        }

        private void UpdateHealthState(int currentHealthPoints)
        {
            // Определение текущего состояния здоровья
            HealthState currentHealthState = currentHealthPoints switch
            {
                >= 70 => HealthState.Normal,
                >= 40 => HealthState.Warning,
                < 40 => HealthState.Danger,
            };

            // Если текущее состояние отличается от предыдущего, обновляем состояние
            if (currentHealthState != _previousHealthState || currentHealthState == HealthState.Normal)
            {
                _previousHealthState = currentHealthState;

                switch (currentHealthState)
                {
                    case HealthState.Normal:
                        _healthBarView.ShowNormal();
                        break;
                    case HealthState.Warning:
                        _healthBarView.ShowWarning();
                        break;
                    case HealthState.Danger:
                        _healthBarView.ShowDanger();
                        break;
                    default:
                        throw new ArgumentOutOfRangeException(); // На случай, если состояние не попадает ни в один из случаев
                }
            }
        }
    }
}