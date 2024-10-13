using UnityEngine;
namespace UI.View
{
    public class PauseScreenView : BasePopupView
    {
        public ButtonDefaultView ResumeButton => _resumeGame;
        public ButtonDefaultView SettingsGame => _settingsGame;
        public ButtonDefaultView ExitGame => _exitGame;
        
        [SerializeField] private ButtonDefaultView _resumeGame;
        [SerializeField] private ButtonDefaultView _settingsGame;
        [SerializeField] private ButtonDefaultView _exitGame;
    }
}