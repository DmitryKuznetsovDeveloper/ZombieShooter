using UnityEngine;
using UnityEngine.SceneManagement;

namespace Game
{
    public sealed class GameLauncher
    {
        private const string LoadingScene = "LoadingScene";
        public void LaunchLoadingScreen() => SceneManager.LoadScene(LoadingScene);
        public void LaunchScene(int value) => SceneManager.LoadScene(value);
        
        public AsyncOperation AsyncLaunchLoadingScreen(int value) => SceneManager.LoadSceneAsync(value);
    }
}