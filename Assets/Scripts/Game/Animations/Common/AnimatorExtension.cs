using UnityEngine;
namespace Game.Animations.Common
{
    public static class AnimatorExtensions
    {
        public static bool IsAnimationPlaying(this Animator animator, int layerIndex, string animationName) =>
            animator.GetCurrentAnimatorStateInfo(layerIndex).IsName(animationName);
    }
}