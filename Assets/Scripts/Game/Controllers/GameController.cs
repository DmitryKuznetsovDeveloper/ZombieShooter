using Game.Systems.InputSystems;
using Plugins.GameCycle;
using Zenject;
namespace Game.Controllers
{
    public sealed class GameController : IInitializable, ITickable
    {
        private readonly GameManager _gameManager;
        private readonly PauseUserInputSystem _pauseUserInputSystem;
        private bool _isGamePaused;

        public GameController(GameManager gameManager, PauseUserInputSystem pauseUserInputSystem)
        {
            _gameManager = gameManager;
            _pauseUserInputSystem = pauseUserInputSystem;
        }

        void IInitializable.Initialize() =>
            _gameManager.StartGame();

        public void Tick()
        {
            if (_pauseUserInputSystem.IsPauseInput) TogglePause();
        }

        private void TogglePause()
        {
            if (_isGamePaused)
                _gameManager.ResumeGame();
            else
                _gameManager.PauseGame();

            _isGamePaused = !_isGamePaused;
        }
    }
}