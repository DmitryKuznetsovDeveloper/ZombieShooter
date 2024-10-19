using Animations.Common;
using Game.AI;
using Game.Animations;
using Game.Animations.Common;
using Game.Components;
using Game.Data;
using Game.Weapon;
using Plugins.Zenject.Source.Install;
using UnityEngine;
using UnityEngine.AI;

namespace DI.ObjectInstallers
{
    public sealed class EnemyInstaller : MonoInstaller
    {
        [SerializeField] private EnemyAIConfig _aiConfig;
        public override void InstallBindings()
        {
            //Animations
            Container.Bind<Animator>().FromComponentInHierarchy().AsSingle().NonLazy();
            Container.Bind<AnimationEventListener>().FromComponentInHierarchy().AsSingle().NonLazy();
            Container.Bind<AIAnimatorSync>().FromComponentInHierarchy().AsSingle().NonLazy();
            Container.BindInterfacesTo<EnemyAnimationEventManager>().AsSingle().NonLazy();

            //Components
            Container.Bind<MeleeWeapon>().FromComponentInHierarchy().AsSingle().NonLazy();
            Container.BindInterfacesAndSelfTo<HealthComponent>().FromComponentInHierarchy().AsSingle().NonLazy();
            Container.BindInterfacesTo<RagdollController>().AsSingle().NonLazy();
            
            //AI
            Container.BindInterfacesAndSelfTo<NavMeshAgent>().FromComponentInHierarchy().AsSingle().NonLazy();
            Container.Bind<EnemyAIConfig>().FromInstance(_aiConfig).AsSingle().NonLazy();
            Container.BindInterfacesTo<EnemyAIController>().AsSingle().NonLazy();
        }
    }
}