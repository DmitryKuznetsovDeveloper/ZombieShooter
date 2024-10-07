using Cysharp.Threading.Tasks;
namespace Game.Weapon
{
    public class Shotgun : BaseWeapon
    {
        protected override async UniTask RechargeProcess()
        {
            _currentClip++;
            _totalAmmo--;
            await UniTask.CompletedTask;
        }
    }
}