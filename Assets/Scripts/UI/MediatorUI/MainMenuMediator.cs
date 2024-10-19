using System;
using Game;
using Plugins.GameCycle;
using UI.UIExtension;
using UI.View;
using UnityEngine;
using Zenject;
namespace UI.MediatorUI
{
    public sealed class MainMenuMediator : IInitializable
    {
        private readonly MainMenuView _mainMenuView;
        private readonly SettingsScreenView _settingsScreenView;
        private readonly ApplicationExiter _applicationExit;
        private readonly GameManager _gameManager;
        private readonly GameLauncher _gameLauncher;

        public MainMenuMediator(MainMenuView mainMenuView, ApplicationExiter applicationExit, GameLauncher gameLauncher, SettingsScreenView settingsScreenView, GameManager gameManager)
        {
            _mainMenuView = mainMenuView;
            _settingsScreenView = settingsScreenView;
            _gameManager = gameManager;
            _applicationExit = applicationExit;
            _gameLauncher = gameLauncher;
        }

        void IInitializable.Initialize()
        {
            _gameManager.StartGame();
            Cursor.lockState = CursorLockMode.Confined;
            Cursor.visible = true;

            SetupButtonAnimations(_mainMenuView.StartButton, _gameLauncher.LaunchLoadingScreen);
            SetupButtonAnimations(_mainMenuView.SettingButton,_settingsScreenView.Show );
            SetupButtonAnimations(_mainMenuView.ExitButton, _applicationExit.ExitApp);
            SetupButtonAnimations(_settingsScreenView.CloseButton, _settingsScreenView.Hide);
        }
    
        private void SetupButtonAnimations(ButtonDefaultView buttonView, Action onClick)
        {
            buttonView.Button.AnimateOnHover(buttonView.SequenceHover, buttonView.TweenParamsHover.EaseOut, buttonView.TweenParamsHover.EaseIn);
            buttonView.Button.AnimateOnClick(buttonView.SequenceClick, buttonView.TweenParamsClick.EaseOut, onClick);
        }
    
    }
}