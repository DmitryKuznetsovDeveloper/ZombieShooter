using Plugins.Zenject.Source.Install;
using UI.View;
namespace DI.SceneInstallers
{
    public sealed class MainMenuSceneInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            //View
            Container.Bind<MainMenuView>().FromComponentInHierarchy().AsSingle().NonLazy();
            Container.Bind<BasePopupView>().FromComponentInHierarchy().AsSingle().NonLazy();
            
            //Mediator
            Container.BindInterfacesTo<MainMenuMediator>().AsSingle().NonLazy();

        }
    }
}