using Cysharp.Threading.Tasks;
namespace Game.Weapon
{
    public class Scar : BaseWeapon
    {
        protected override async UniTask RechargeProcess()
        {
            int ammoNeeded = _weaponConfig.MagazineCapacity - _currentClip;
            if (_totalAmmo >= ammoNeeded)
            {
                _totalAmmo -= ammoNeeded;
                _currentClip = _weaponConfig.MagazineCapacity;
            }
            else
            {
                _currentClip += _totalAmmo;
                _totalAmmo = 0;
            }
            await UniTask.CompletedTask;
        }
    }
}