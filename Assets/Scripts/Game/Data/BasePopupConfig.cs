using System;
using Code.Tween;
using UnityEngine;
namespace Game.Data
{
    [CreateAssetMenu(fileName = "BasePopupConfig", menuName = "Configs/Animations/BasePopupConfig", order = 0)]
    public class BasePopupConfig : ScriptableObject
    {
        public BasePopupSettings ShowState;
        public BasePopupSettings HideState;
    }
}

[Serializable]
public class BasePopupSettings
{
    [Range(0, 1)] public float MainFade;
    public float TargetScale;
    public float StartScale;
    public TweenParamsOut Params;
}