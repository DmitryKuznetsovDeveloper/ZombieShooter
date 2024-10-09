using System;
using Game;
using UI.UIExtension;
using UI.View;
using UnityEngine;
using Zenject;
public sealed class MainMenuMediator : IInitializable
{
    private readonly MainMenuView _mainMenuView;
    private readonly ApplicationExiter _applicationExit;
    private readonly GameLauncher _gameLauncher;
    
    public MainMenuMediator(MainMenuView mainMenuView, ApplicationExiter applicationExit, GameLauncher gameLauncher)
    {
        _mainMenuView = mainMenuView;
        _applicationExit = applicationExit;
        _gameLauncher = gameLauncher;
    }

     void IInitializable.Initialize()
    {
        Cursor.lockState = CursorLockMode.Confined;
        Cursor.visible = true;
        _mainMenuView.StartButton.Button.AnimateOnHover(_mainMenuView.StartButton.SequenceHover,_mainMenuView.StartButton.TweenParamsHover.EaseOut,_mainMenuView.StartButton.TweenParamsHover.EaseIn);
        _mainMenuView.StartButton.Button.AnimateOnClick(_mainMenuView.StartButton.SequenceClick,_mainMenuView.StartButton.TweenParamsHover.EaseOut,_gameLauncher.LaunchGame);
        _mainMenuView.StartButton.Button.AnimateOnClick(_mainMenuView.StartButton.SequenceClick,_mainMenuView.StartButton.TweenParamsHover.EaseOut,_gameLauncher.LaunchGame);
        
        _mainMenuView.SettingButton.Button.AnimateOnHover(_mainMenuView.SettingButton.SequenceHover,_mainMenuView.SettingButton.TweenParamsHover.EaseOut,_mainMenuView.SettingButton.TweenParamsHover.EaseIn);
       
        _mainMenuView.ExitButton.Button.AnimateOnHover(_mainMenuView.ExitButton.SequenceHover,_mainMenuView.ExitButton.TweenParamsHover.EaseOut,_mainMenuView.ExitButton.TweenParamsHover.EaseIn);
        _mainMenuView.ExitButton.Button.AnimateOnClick(_mainMenuView.ExitButton.SequenceClick,_mainMenuView.ExitButton.TweenParamsHover.EaseOut,_applicationExit.ExitApp);
    }
}
