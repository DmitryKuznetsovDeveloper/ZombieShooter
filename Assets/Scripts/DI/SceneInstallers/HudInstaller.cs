using Game.Weapon;
using Plugins.Zenject.Source.Install;
using UI.MediatorUI;

public sealed class HudInstaller : MonoInstaller
{
    public override void InstallBindings()
    {
        //View
        Container.Bind<HealthBarView>().FromComponentInHierarchy().AsSingle().NonLazy();
        Container.Bind<WeaponsView>().FromComponentInHierarchy().AsSingle().NonLazy();

        Container.BindInterfacesAndSelfTo<CharacterInstaller>().FromComponentInHierarchy().AsSingle().NonLazy();
        Container.BindInterfacesTo<HealthBarMediator>().AsSingle().NonLazy();
        Container.BindInterfacesTo<WeaponsMediator>().AsSingle().NonLazy();
    }
}