using UnityEditor;

namespace GamePlay
{
    public sealed class ApplicationExiter
    {
        public void ExitApp()
        {
#if UNITY_EDITOR
            EditorApplication.isPlaying = false;
#else
            Application.Quit(0);
#endif
        }
    }
}