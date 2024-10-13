using Plugins.Zenject.Source.Install;
using UI.MediatorUI;
using UI.View;
namespace DI.SceneInstallers
{
    public class LoadingSceneInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            Container.Bind<LoadingScreenView>().FromComponentInHierarchy().AsSingle().NonLazy();
            Container.BindInterfacesTo<LoadingScreenMediator>().AsSingle().NonLazy();
        }
    }
}