﻿using System.Threading;
using Cysharp.Threading.Tasks;
using Game.Animations.Common;
using UnityEngine;
namespace Game.Animations
{
    public sealed class WeaponBaseAnimations
    {
        private static readonly int Recharge = Animator.StringToHash("Recharge");
        private static readonly int Shoot = Animator.StringToHash("Shoot");

        private const string WeaponLayer = "WeaponReload";
        private const string RechargeStart = "RechargeStart";
        private const string RechargeBase = "Recharge";
        private const string RechargeEnd = "RechargeEnd";

        private int _weaponReloadIndex;
    
        public void ShowShoot(Animator animator) => animator.SetTrigger(Shoot);

        public async UniTask WaitForRechargeAnimationEnd(Animator animator, CancellationToken cancellationToken = default)
        {
            animator.SetTrigger(Recharge);
            _weaponReloadIndex = animator.GetLayerIndex(WeaponLayer);
            if (_weaponReloadIndex == -1) return;

            // Ожидаем окончания анимации на указанном слое
            await UniTask.WaitUntil(() => !animator.IsAnimationPlaying( _weaponReloadIndex, RechargeStart), cancellationToken: cancellationToken);
            await UniTask.WaitUntil(() => !animator.IsAnimationPlaying( _weaponReloadIndex, RechargeBase), cancellationToken: cancellationToken);
            await UniTask.WaitUntil(() => !animator.IsAnimationPlaying( _weaponReloadIndex, RechargeEnd), cancellationToken: cancellationToken);
        }
    }
}