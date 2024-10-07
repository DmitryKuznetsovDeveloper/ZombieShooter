using Plugins.Zenject.Source.Install;
using UnityEngine;
using UnityEngine.SceneManagement;
using Zenject;
namespace Plugins.Zenject.OptionalExtras.IntegrationTests.SceneTests.TestDestructionOrder
{
    public class SceneChangeHandler : ITickable
    {
        public void Tick()
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                SceneManager.LoadScene("EmptyScene", LoadSceneMode.Single);
            }
        }
    }

    public class Scene2Installer : MonoInstaller<Scene2Installer>
    {
        public override void InstallBindings()
        {
            Container.BindInterfacesTo<SceneChangeHandler>().AsSingle();
            Container.BindInterfacesTo<FooDisposable2>().AsSingle();
        }
    }
}
