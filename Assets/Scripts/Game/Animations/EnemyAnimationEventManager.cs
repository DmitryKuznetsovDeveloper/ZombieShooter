using Animations.Common;
using Game.Weapon;
using Plugins.GameCycle;
namespace Game.Animations
{
    public sealed class EnemyAnimationEventManager : IGameStartListener
    {
        private readonly AnimationEventListener _animationEventListener;
        private readonly MeleeWeapon _meleeWeapon;
        
        public EnemyAnimationEventManager(AnimationEventListener animationEventListener, MeleeWeapon meleeWeapon)
        {
            _animationEventListener = animationEventListener;
            _meleeWeapon = meleeWeapon;
        }
        public void OnStartGame()
        {
            _animationEventListener.AddEvent("event_attack", _meleeWeapon.MeleeAttack);
        }
    }
}