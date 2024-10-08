using UI.AnimationUI;
using UnityEngine
    ;
namespace UI.View
{
    public class MainMenuView : MonoBehaviour
    {
        [SerializeField] private ButtonDefaultView _startButton;
        [SerializeField] private ButtonDefaultView _settingButton;
        [SerializeField] private ButtonDefaultView _exitButton;

        public ButtonDefaultView StartButton => _startButton;
        public ButtonDefaultView SettingButton => _settingButton;
        public ButtonDefaultView ExitButton => _exitButton;
    }
}