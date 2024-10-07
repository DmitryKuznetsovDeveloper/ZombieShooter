using Plugins.GameCycle;
using Zenject;

namespace Controllers
{
    public sealed class GameController : IInitializable
    {
        private readonly GameManager _gameManager;

        public GameController(GameManager gameManager) => 
            _gameManager = gameManager;

        void IInitializable.Initialize() =>
            _gameManager.StartGame();
    }
}