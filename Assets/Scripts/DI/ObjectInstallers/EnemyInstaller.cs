using Animations.Common;
using Game.Animations;
using Game.Weapon;
using Plugins.Zenject.Source.Install;
using UnityEngine;
namespace DI.ObjectInstallers
{
    public class EnemyInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            Container.Bind<Animator>().FromComponentInHierarchy().AsSingle().NonLazy();
            Container.Bind<MeleeWeapon>().FromComponentInHierarchy().AsSingle().NonLazy();
            Container.Bind<AnimationEventListener>().FromComponentInHierarchy().AsSingle().NonLazy();
            Container.BindInterfacesTo<EnemyAnimationEventManager>().AsSingle().NonLazy();
        }
    }
}