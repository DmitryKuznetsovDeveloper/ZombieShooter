using System.Collections.Generic;
using DI.ObjectInstallers;
using Game.Pool;
using Game.Weapon;
using Plugins.GameCycle;
using UI.View;
using UnityEngine;
using Zenject;

namespace UI.MediatorUI
{
    public sealed class WeaponsMediator : IInitializable, IGameFinishListener
    {
        private readonly CharacterController _character;
        private readonly WeaponInventoryView _weaponInventoryView;
        private readonly WeaponSelector _weaponSelector;
        private BaseWeapon[] _baseWeapons;
        private List<WeaponTemplateView> _weaponViews;
        private WeaponTemplateView _selectedWeapon;

        private ObjectPool<WeaponTemplateView> _weaponsPool;
        public WeaponsMediator(CharacterController character, WeaponInventoryView weaponInventoryView, WeaponSelector weaponSelector)
        {
            _character = character;
            _weaponInventoryView = weaponInventoryView;
            _weaponSelector = weaponSelector;
        }

        public void Initialize()
        {
            _weaponViews = new List<WeaponTemplateView>();
            _baseWeapons = _character.GetComponentsInChildren<BaseWeapon>(true);
            _weaponsPool = new ObjectPool<WeaponTemplateView>(_weaponInventoryView.WeaponViewPrefab, _baseWeapons.Length, _weaponInventoryView.Container);
            SpawnWeapon(_baseWeapons, _weaponsPool);
            _weaponSelector.OnChangeWeapon += PlayAnimation;
            BindAmmoChangeEvents(_baseWeapons);
        }
        
        private void BindAmmoChangeEvents(BaseWeapon[] baseWeapons)
        {
            for (int i = 0; i < _weaponViews.Count; i++)
                baseWeapons[i].OnChangeAmmo += _weaponViews[i].SetWeaponAmmoLabel;
        }
        private void UnbindAmmoChangeEvents(BaseWeapon[] baseWeapons)
        {
            for (int i = 0; i < _weaponViews.Count; i++)
                baseWeapons[i].OnChangeAmmo -= _weaponViews[i].SetWeaponAmmoLabel;
        }

        private void PlayAnimation(BaseWeapon targetWeapon)
        {
            _selectedWeapon.PlayAnimation(WeaponTemplateView.WeaponViewAnimationState.Normal);
            for (int i = 0; i < _baseWeapons.Length; i++)
            {
                if (_baseWeapons[i] == targetWeapon)
                {
                    var weaponView = _weaponViews[i];
                    weaponView.PlayAnimation(WeaponTemplateView.WeaponViewAnimationState.Selected);
                    _selectedWeapon = weaponView;
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
                    item.PlayAnimation(WeaponTemplateView.WeaponViewAnimationState.Selected);
                    _selectedWeapon = item;
                }
                else
                {
                    item.PlayAnimation(WeaponTemplateView.WeaponViewAnimationState.Normal);
                }
                item.SetSpriteWeapon(weapons.WeaponConfig.WeaponIcon);
                _weaponViews.Add(item);
            }
        }
        public void OnFinishGame()
        {
            UnbindAmmoChangeEvents(_baseWeapons);
            _weaponSelector.OnChangeWeapon -= PlayAnimation;
        }
    }
}