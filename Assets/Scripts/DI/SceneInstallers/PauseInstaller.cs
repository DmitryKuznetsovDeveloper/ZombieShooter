using Game.Controllers;
using Game.Systems.InputSystems;
using Plugins.Zenject.Source.Install;
namespace DI.SceneInstallers
{
    public sealed class PauseInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            Container.BindInterfacesAndSelfTo<PauseUserInputSystem>().AsSingle().NonLazy();
            Container.BindInterfacesAndSelfTo<GameController>().AsSingle().NonLazy();
        }
    }
}