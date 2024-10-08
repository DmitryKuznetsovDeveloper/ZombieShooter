using Plugins.Zenject.Source.Install;
using UI.AnimationUI;
using UI.View;
namespace DI.SceneInstallers
{
    public class MainMenuSceneInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            //View
            Container.Bind<MainMenuView>().FromComponentInHierarchy().AsSingle().NonLazy();
            
            //Mediator
            Container.BindInterfacesTo<MainMenuMediator>().AsSingle().NonLazy();

        }
    }
}