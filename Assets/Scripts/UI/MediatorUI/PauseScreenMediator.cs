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
        private readonly SettingsScreenView _settingsScreen;

        public PauseScreenMediator(PauseScreenView pauseScreenView, GameManager gameManager, SettingsScreenView settingsScreen, ApplicationExiter applicationExiter)
        {
            _pauseScreenView = pauseScreenView;
            _gameManager = gameManager;
            _settingsScreen = settingsScreen;
            _applicationExiter = applicationExiter;
        }
        
        public void Initialize()
        {
            SetupButtonAnimations(_pauseScreenView.ResumeButton, _gameManager.ResumeGame);
            SetupButtonAnimations(_pauseScreenView.SettingsGame, ShowSettingScreen);
            SetupButtonAnimations(_pauseScreenView.ExitGame, _applicationExiter.ExitApp);
            SetupButtonAnimations(_pauseScreenView.CloseButton, _gameManager.ResumeGame);
            SetupButtonAnimations(_settingsScreen.CloseButton, HideSettingScreen);
        }

        private void SetupButtonAnimations(ButtonDefaultView buttonView, Action onClick)
        {
            buttonView.Button.AnimateOnHover(buttonView.SequenceHover, buttonView.TweenParamsHover.EaseOut, buttonView.TweenParamsHover.EaseIn);
            buttonView.Button.AnimateOnClick(buttonView.SequenceClick, buttonView.TweenParamsClick.EaseOut, onClick);
        }

        public void OnPauseGame() => _pauseScreenView.Show();
        
        public void OnResumeGame()
        {
            _pauseScreenView.Hide();
            _settingsScreen.Hide();
        }

        private void ShowSettingScreen()
        { 
            _pauseScreenView.Hide(); 
            _settingsScreen.Show();
        }

        private void HideSettingScreen()
        {
            _settingsScreen.Hide();
            _pauseScreenView.Show();
        }
    }
}