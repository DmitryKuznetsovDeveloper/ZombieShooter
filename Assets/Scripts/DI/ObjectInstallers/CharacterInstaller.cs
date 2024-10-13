using Controllers;
using Game.Controllers;
using Game.Data;
using Game.Systems.InputSystems;
using Game.Weapon;
using Plugins.Zenject.Source.Install;
using UnityEngine;

public sealed class CharacterInstaller : MonoInstaller
{
    [SerializeField] private CharacterMovementConfig _characterMovementConfig;
    [SerializeField] private Transform _bodyTransform;
    [SerializeField] private BaseWeapon[] _weapons;
    public override void InstallBindings()
    {
        //Configs
        Container.Bind<CharacterMovementConfig>().FromInstance(_characterMovementConfig).AsSingle().NonLazy();
        
        //Systems
        Container.BindInterfacesAndSelfTo<MouseUserInputSystem>().AsSingle().NonLazy();
        Container.BindInterfacesAndSelfTo<MoveUserInputSystem>().AsSingle().NonLazy();
        Container.BindInterfacesAndSelfTo<RunUserInputSystem>().AsSingle().NonLazy();
        Container.BindInterfacesAndSelfTo<JumpUserInputSystem>().AsSingle().NonLazy();
        Container.BindInterfacesAndSelfTo<ReloadUserInputSystem>().AsSingle().NonLazy();
        Container.BindInterfacesAndSelfTo<ShootUserInputSystem>().AsSingle().NonLazy();
        Container.BindInterfacesAndSelfTo<PauseUserInputSystem>().AsSingle().NonLazy();
        
        
        //Components
        Container.Bind<WeaponBaseAnimations>().AsSingle().NonLazy();
        Container.Bind<WeaponSelector>().AsSingle().NonLazy();
        Container.Bind<CharacterController>().FromComponentInHierarchy().AsSingle().NonLazy();
        Container.Bind<BaseWeapon[]>().FromInstance(_weapons).AsSingle().NonLazy();
        Container.BindInterfacesAndSelfTo<CharacterMovement>().AsSingle().WithArguments(transform,_bodyTransform).NonLazy();
        
        //Controllers
        Container.BindInterfacesTo<CharacterMovementController>().AsSingle().NonLazy();
        Container.BindInterfacesTo<WeaponController>().AsSingle().NonLazy();

    }
}