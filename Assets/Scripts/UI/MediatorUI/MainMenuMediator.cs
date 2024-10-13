using System;
using Game;
using Plugins.GameCycle;
using UI.UIExtension;
using UI.View;
using UnityEngine;
using Zenject;

public sealed class MainMenuMediator : IInitializable
{
    private readonly MainMenuView _mainMenuView;
    private readonly BasePopupView _basePopupView;
    private readonly ApplicationExiter _applicationExit;
    private readonly GameManager _gameManager;
    private readonly GameLauncher _gameLauncher;

    public MainMenuMediator(MainMenuView mainMenuView, ApplicationExiter applicationExit, GameLauncher gameLauncher, BasePopupView basePopupView, GameManager gameManager)
    {
        _mainMenuView = mainMenuView;
        _basePopupView = basePopupView;
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
        SetupButtonAnimations(_mainMenuView.SettingButton, _basePopupView.Show);
        SetupButtonAnimations(_mainMenuView.ExitButton, _applicationExit.ExitApp);
        SetupButtonAnimations(_basePopupView.CloseButton, _basePopupView.Hide);
    }
    
    private void SetupButtonAnimations(ButtonDefaultView buttonView, Action onClick)
    {
        buttonView.Button.AnimateOnHover(buttonView.SequenceHover, buttonView.TweenParamsHover.EaseOut, buttonView.TweenParamsHover.EaseIn);
        buttonView.Button.AnimateOnClick(buttonView.SequenceClick, buttonView.TweenParamsClick.EaseOut, onClick);
    }
}