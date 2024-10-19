using System;
using UI.ParamsTween;
using UnityEngine;
namespace Game.Data
{
    [CreateAssetMenu(fileName = "HealthBarConfig", menuName = "Configs/Animations/HealthBarConfig", order = 0)]
    public class HealthBarConfig : ScriptableObject
    {
        public HealthBarSettings NormalStae;
        public HealthBarSettings WarninglStae;
        public HealthBarSettings DangerStae;
    }
}

[Serializable]
public class HealthBarSettings
{
    public Color HealthIcn;
    public Color HealthBarFill;
    public float ScaleFactor;
    public TweenParamsOut Params;
}