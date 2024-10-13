using System.Collections.Generic;
using Game.Pool;
using Game.Weapon;
using Plugins.GameCycle;
using UnityEngine;
using Zenject;

namespace UI.MediatorUI
{
    public sealed class WeaponsMediator : IInitializable, IGameTickable, IGameFinishListener
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
            Debug.Log(_baseWeapons.Length);
            SpawnWeapon(_baseWeapons, _weaponsPool);
            _weaponSelector.OnChangeWeapon += PlayAnimationForWeapon;
        }
        public void Tick(float deltaTime) => UpdateAmmoLabel();
        private void UpdateAmmoLabel()
        {
            for (int i = 0; i < _weaponViews.Count; i++)
                _weaponViews[i].SetWeaponAmmoLabel(_baseWeapons[i].CurrentClip, 
                    _baseWeapons[i].CurrentTotalAmmo, 
                    _baseWeapons[i].WeaponConfig.RichTextAmmo);
        }

        private void PlayAnimationForWeapon(BaseWeapon targetWeapon)
        {
            _currentSelectedWeaponView.ShowNormal();
            for (int i = 0; i < _baseWeapons.Length; i++)
            {
                if (_baseWeapons[i] == targetWeapon)
                {
                    var weaponView = _weaponViews[i];
                    weaponView.ShowSelected();
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
                item.InitSequence();
                if (i == 0)
                {
                    item.ShowSelected();
                    _currentSelectedWeaponView = item;
                }
                else
                {
                    item.ShowNormal();
                }
                item.SetSpriteWeapon(weapons.WeaponConfig.WeaponIcon);
                item.SetWeaponAmmoLabel(weapons.CurrentClip, weapons.CurrentTotalAmmo, weapons.WeaponConfig.RichTextAmmo);
                _weaponViews.Add(item);
            }
        }
        public void OnFinishGame() => _weaponSelector.OnChangeWeapon -= PlayAnimationForWeapon;
    }
}