using Plugins.Zenject.Source.Install;
using UI.View;
namespace DI.SceneInstallers
{
    public class MainMenuSceneInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            //View
            Container.Bind<MainMenuView>().FromComponentInHierarchy().AsSingle().NonLazy();
            Container.Bind<SettingsScreenView>().FromComponentInHierarchy().AsSingle().NonLazy();
            
            //Mediator
            Container.BindInterfacesTo<MainMenuMediator>().AsSingle().NonLazy();

        }
    }
}