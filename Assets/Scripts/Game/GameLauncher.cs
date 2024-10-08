using UnityEngine.SceneManagement;

namespace Game
{
    public sealed class GameLauncher
    {
        private const string LevelOne = "LevelOne";
        public void LaunchGame() => 
            SceneManager.LoadScene(LevelOne);
    }
}