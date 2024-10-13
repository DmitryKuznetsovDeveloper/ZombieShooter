using UnityEngine;

namespace UI.View
{
    public sealed class MainMenuView : MonoBehaviour
    {
        public ButtonDefaultView StartButton => _startButton;
        public ButtonDefaultView SettingButton => _settingButton;
        public ButtonDefaultView ExitButton => _exitButton;
        
        [SerializeField] private ButtonDefaultView _startButton;
        [SerializeField] private ButtonDefaultView _settingButton;
        [SerializeField] private ButtonDefaultView _exitButton;
    }
}