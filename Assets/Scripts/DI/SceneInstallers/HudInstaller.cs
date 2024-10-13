using Plugins.Zenject.Source.Install;
using UI.MediatorUI;
using UI.View;

public sealed class HudInstaller : MonoInstaller
{
    public override void InstallBindings()
    {
        //View
        Container.Bind<HealthBarView>().FromComponentInHierarchy().AsSingle().NonLazy();
        Container.Bind<WeaponsView>().FromComponentInHierarchy().AsSingle().NonLazy();
        Container.Bind<PauseScreenView>().FromComponentInHierarchy().AsSingle().NonLazy();
        Container.Bind<SettingsScreenView>().FromComponentInHierarchy().AsSingle().NonLazy();

        Container.BindInterfacesAndSelfTo<CharacterInstaller>().FromComponentInHierarchy().AsSingle().NonLazy();
        Container.BindInterfacesTo<HealthBarMediator>().AsSingle().NonLazy();
        Container.BindInterfacesTo<WeaponsMediator>().AsSingle().NonLazy();
        Container.BindInterfacesTo<PauseScreenMediator>().AsSingle().NonLazy();
    }
}