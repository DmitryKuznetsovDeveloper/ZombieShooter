﻿using System.Collections.Generic;
using Game.Pool;
using Game.Weapon;
using Plugins.GameCycle;
using Zenject;

namespace UI.MediatorUI
{
    public sealed class WeaponsMediator : IInitializable, IGameFinishListener
    {
        private readonly CharacterInstaller _characterInstaller;
        private readonly WeaponsView _weaponView;
        private readonly WeaponSelector _weaponSelector;
        private BaseWeapon[] _baseWeapons;
        private List<WeaponTemplateView> _weaponViews;
        private WeaponTemplateView _currentSelectedWeaponView;

        private ObjectPool<WeaponTemplateView> _weaponsPool;
        public WeaponsMediator(CharacterInstaller characterInstaller, WeaponsView weaponView, WeaponSelector weaponSelector)
        {
            _characterInstaller = characterInstaller;
            _weaponView = weaponView;
            _weaponSelector = weaponSelector;
        }

        public void Initialize()
        {
            _weaponViews = new List<WeaponTemplateView>();
            _baseWeapons = _characterInstaller.GetComponentsInChildren<BaseWeapon>(true);
            _weaponsPool = new ObjectPool<WeaponTemplateView>(_weaponView.WeaponViewPrefab, _baseWeapons.Length, _weaponView.Container);
            SpawnWeapon(_baseWeapons, _weaponsPool);
            _weaponSelector.OnChangeWeapon += PlayAnimationForWeapon;
            SubscriptionAmmo(_baseWeapons);
        }
        
        private void SubscriptionAmmo(BaseWeapon[] baseWeapons)
        {
            for (int i = 0; i < _weaponViews.Count; i++)
                baseWeapons[i].OnChangeAmmo += _weaponViews[i].SetWeaponAmmoLabel;
        }
        private void UnsubscriptionAmmo(BaseWeapon[] baseWeapons)
        {
            for (int i = 0; i < _weaponViews.Count; i++)
                baseWeapons[i].OnChangeAmmo -= _weaponViews[i].SetWeaponAmmoLabel;
        }

        private void PlayAnimationForWeapon(BaseWeapon targetWeapon)
        {
            _currentSelectedWeaponView.PlayForwardAnimation(WeaponTemplateView.WeaponViewAnimationState.Normal);
            for (int i = 0; i < _baseWeapons.Length; i++)
            {
                if (_baseWeapons[i] == targetWeapon)
                {
                    var weaponView = _weaponViews[i];
                    weaponView.PlayForwardAnimation(WeaponTemplateView.WeaponViewAnimationState.Selected);
                    _currentSelectedWeaponView = weaponView;
                    break;
                }
            }
        }
        private void SpawnWeapon(BaseWeapon[] baseWeapons, ObjectPool<WeaponTemplateView> pool)
        {
            for (int i = 0; i < baseWeapons.Length; i++)
            {
                var item = pool.GetObject();
                var weapons = _baseWeapons[i];
                if (i == 0)
                {
                    item.PlayForwardAnimation(WeaponTemplateView.WeaponViewAnimationState.Selected);
                    _currentSelectedWeaponView = item;
                }
                else
                {
                    item.PlayForwardAnimation(WeaponTemplateView.WeaponViewAnimationState.Normal);
                }
                item.SetSpriteWeapon(weapons.WeaponConfig.WeaponIcon);
                _weaponViews.Add(item);
            }
        }
        public void OnFinishGame()
        {
            UnsubscriptionAmmo(_baseWeapons);
            _weaponSelector.OnChangeWeapon -= PlayAnimationForWeapon;
        }
    }
}