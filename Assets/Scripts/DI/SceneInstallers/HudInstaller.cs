using Plugins.Zenject.Source.Install;
using UI;
using UI.MediatorUI;
using UI.View;

namespace DI.SceneInstallers
{
    public sealed class HudInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            //View
            Container.Bind<HealthBarView>().FromComponentInHierarchy().AsSingle().NonLazy();
            Container.Bind<WeaponInventoryView>().FromComponentInHierarchy().AsSingle().NonLazy();
            Container.Bind<PauseScreenView>().FromComponentInHierarchy().AsSingle().NonLazy();
            Container.Bind<SettingsScreenView>().FromComponentInHierarchy().AsSingle().NonLazy();
        
            Container.BindInterfacesTo<HealthBarMediator>().AsSingle().NonLazy();
            Container.BindInterfacesTo<WeaponsMediator>().AsSingle().NonLazy();
            Container.BindInterfacesTo<PauseScreenMediator>().AsSingle().NonLazy();
            
            Container.BindInterfacesAndSelfTo<PopupManager>().AsSingle().NonLazy();
        }
    }
}