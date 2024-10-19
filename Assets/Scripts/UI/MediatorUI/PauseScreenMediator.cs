using System;
using Game;
using Plugins.GameCycle;
using UI.UIExtension;
using UI.View;
using Zenject;

namespace UI.MediatorUI
{
    public sealed class PauseScreenMediator : IInitializable, IGamePauseListener, IGameResumeListener
    {
        private readonly PauseScreenView _pauseScreenView;
        private readonly GameManager _gameManager;
        private readonly ApplicationExiter _applicationExiter;
        private readonly SettingsScreenView _settingsScreenView;
        private readonly PopupManager _popupManager;

        public PauseScreenMediator(PauseScreenView pauseScreenView, GameManager gameManager, SettingsScreenView settingsScreenView, ApplicationExiter applicationExiter, PopupManager popupManager)
        {
            _pauseScreenView = pauseScreenView;
            _gameManager = gameManager;
            _settingsScreenView = settingsScreenView;
            _applicationExiter = applicationExiter;
            _popupManager = popupManager;
        }

        public void Initialize()
        {
            SetupButtonAnimations(_pauseScreenView.ResumeButton, _gameManager.ResumeGame);
            SetupButtonAnimations(_pauseScreenView.SettingsGame, ShowSettingScreen);
            SetupButtonAnimations(_pauseScreenView.ExitGame, _applicationExiter.ExitApp);
            SetupButtonAnimations(_pauseScreenView.CloseButton, _gameManager.ResumeGame);
            SetupButtonAnimations(_settingsScreenView.CloseButton, HideSettingScreen);
            OnResumeGame();
        }

        private void SetupButtonAnimations(ButtonDefaultView buttonView, Action onClick)
        {
            buttonView.Button.AnimateOnHover(buttonView.SequenceHover, buttonView.TweenParamsHover.EaseOut, buttonView.TweenParamsHover.EaseIn);
            buttonView.Button.AnimateOnClick(buttonView.SequenceClick, buttonView.TweenParamsClick.EaseOut, onClick);
        }

        public void OnPauseGame() => _popupManager.ShowPopup(_pauseScreenView);

        public void OnResumeGame() => _popupManager.CloseCurrentPopup();

        private void ShowSettingScreen() => _popupManager.ShowPopup(_settingsScreenView);

        private void HideSettingScreen() => _popupManager.CloseCurrentPopup();
    }
}