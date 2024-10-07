using UnityEngine.SceneManagement;

namespace GamePlay
{
    public sealed class GameLauncher
    {
        private const string LevelOne = "Level01";
        public void LaunchGame() => 
            SceneManager.LoadScene(LevelOne);
    }
}