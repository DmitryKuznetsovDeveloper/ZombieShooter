using Game;
using Game.Weapon;
using Plugins.GameCycle;
using Plugins.Zenject.Source.Install;
using UnityEngine;
namespace DI.ProjectInstaller
{
    [CreateAssetMenu(
        fileName = "GameCoreInstaller",
        menuName = "Installers/GameCoreInstaller"
    )]
    public sealed class GameCoreInstaller : ScriptableObjectInstaller
    {
        public override void InstallBindings()
        {
            BindInstances();
        }

        private void BindInstances()
        {
            Container.Bind<GameManager>().AsSingle();
            Container.BindInterfacesAndSelfTo<Camera>().FromComponentsInHierarchy().AsSingle().NonLazy();
            Container.BindInterfacesAndSelfTo<ApplicationExiter>().AsSingle().NonLazy();
            Container.BindInterfacesAndSelfTo<GameLauncher>().AsSingle().NonLazy();
            Container.Bind<WeaponSelector>().AsSingle().NonLazy();
        }
    }
}