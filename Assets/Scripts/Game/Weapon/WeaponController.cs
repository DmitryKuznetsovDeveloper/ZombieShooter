using Cysharp.Threading.Tasks;
using Game.Systems.InputSystems;
using Plugins.GameCycle;
using Zenject;

namespace Game.Weapon
{
    public sealed class WeaponController : IInitializable, IGameTickable
    {
        private readonly BaseWeapon[] _weapons;
        private readonly WeaponSelector _weaponSelector;
        private readonly ReloadUserInputSystem _reloadUserInputSystem;
        private readonly ShootUserInputSystem _shootUserInputSystem;
        private readonly MouseUserInputSystem _mouseUserInputSystem;
        
        public WeaponController(
            BaseWeapon[] weapons,
            WeaponSelector weaponSelector,
            ReloadUserInputSystem reloadUserInputSystem,
            ShootUserInputSystem shootUserInputSystem, 
            MouseUserInputSystem mouseUserInputSystem)
        {
            _weapons = weapons;
            _weaponSelector = weaponSelector;
            _reloadUserInputSystem = reloadUserInputSystem;
            _shootUserInputSystem = shootUserInputSystem;
            _mouseUserInputSystem = mouseUserInputSystem;
        }
        void IInitializable.Initialize()
        {
            _weaponSelector.Init(_weapons);
            _weaponSelector.GetWeapon();
        }
        async void IGameTickable.Tick(float time)
        {

            await AsyncRecharge();

            SelectWeapon();

            Shoot();

            ChangeAimCamera();
        }
        private void ChangeAimCamera() => _weaponSelector.CurrentBaseWeapon.BaseCamera.gameObject.SetActive(!_shootUserInputSystem.IsAimInput);
        private void SelectWeapon()
        {
            if (_mouseUserInputSystem.MouseScroll.y >= 0.1f)
                _weaponSelector.Next();

            if (_mouseUserInputSystem.MouseScroll.y <= -0.1f)
                _weaponSelector.Previous();
        }
        private void Shoot()
        {
            if (_shootUserInputSystem.IsShootInput)
                _weaponSelector.CurrentBaseWeapon.Shoot();
        }
        private async UniTask AsyncRecharge()
        {
            if (_reloadUserInputSystem.IsReloadInput && _weaponSelector.CurrentBaseWeapon)
                await _weaponSelector.CurrentBaseWeapon.Recharge();
        }
    }
}