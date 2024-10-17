using System;
using Code.Tween;
using UnityEngine;
namespace Game.Data
{
    [CreateAssetMenu(fileName = "WeaponInfoBackgroundConfig", menuName = "Configs/Animations/WeaponInfoBackgroundConfig", order = 0)]
    public class WeaponInfoBackgroundConfig : ScriptableObject
    {
        public WeaponInfoBackgroundSettings NormalState;
        public WeaponInfoBackgroundSettings SelectedState;
    }
}

[Serializable]
public class WeaponInfoBackgroundSettings
{
    [Header("State")]
    public float scaleFactorWeapon;
    public Vector2 SizeDeltaGradientFrom;
    public Vector2 SizeDeltaGradientTarget;
    [Range(0,1)]public float MainFade;
    [Range(0,1)]public float FrameSelectedFade;
    public Color FrameSelectedColor;
    public Color BgMainColor;
    public Color BgGradientColor;
    public TweenParamsOut Params;
}