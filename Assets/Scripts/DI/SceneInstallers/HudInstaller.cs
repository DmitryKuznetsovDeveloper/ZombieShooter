using Plugins.Zenject.Source.Install;
using UI.MediatorUI;

public sealed class HudInstaller : MonoInstaller
{
    public override void InstallBindings()
    {
        //View
        Container.Bind<HealthBarView>().FromComponentInHierarchy().AsSingle().NonLazy();
        Container.Bind<WeaponInfoView>().FromComponentInHierarchy().AsSingle().NonLazy();

        Container.BindInterfacesAndSelfTo<CharacterInstaller>().FromComponentInHierarchy().AsSingle().NonLazy();
        Container.BindInterfacesTo<HudMediator>().AsSingle().NonLazy();
    }
}