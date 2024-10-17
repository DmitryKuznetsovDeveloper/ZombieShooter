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

        public PauseScreenMediator(PauseScreenView pauseScreenView, GameManager gameManager, SettingsScreenView settingsScreenView, ApplicationExiter applicationExiter)
        {
            _pauseScreenView = pauseScreenView;
            _gameManager = gameManager;
            _settingsScreenView = settingsScreenView;
            _applicationExiter = applicationExiter;
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

        public void OnPauseGame()
        {
            _pauseScreenView.PlayForwardAnimation(BasePopupView.BasePopupState.Show);
            _pauseScreenView.SwitchCanvasGroupEnable(true);
        }

        public void OnResumeGame()
        {
            _pauseScreenView.PlayForwardAnimation(BasePopupView.BasePopupState.Hide);
            _pauseScreenView.SwitchCanvasGroupEnable(false);
            _settingsScreenView.PlayForwardAnimation(BasePopupView.BasePopupState.Hide);
            _settingsScreenView.SwitchCanvasGroupEnable(false);
        }

        private void ShowSettingScreen()
        { 
            _pauseScreenView.PlayForwardAnimation(BasePopupView.BasePopupState.Hide); 
            _pauseScreenView.SwitchCanvasGroupEnable(false);
            _settingsScreenView.PlayForwardAnimation(BasePopupView.BasePopupState.Show);
            _settingsScreenView.SwitchCanvasGroupEnable(true);
        }

        private void HideSettingScreen()
        {
            _pauseScreenView.PlayForwardAnimation(BasePopupView.BasePopupState.Show);
            _pauseScreenView.SwitchCanvasGroupEnable(true);
            _settingsScreenView.PlayForwardAnimation(BasePopupView.BasePopupState.Hide);
            _settingsScreenView.SwitchCanvasGroupEnable(false);
        }
    }
}