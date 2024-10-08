using System;
using DG.Tweening;

namespace Code.Tween
{
    [Serializable]
    public class TweenParamsOutIn
    {
        public float Duration;
        public float Delay;
        public Ease EaseOut;
        public Ease EaseIn;
    }

    [Serializable]
    public class TweenParamsOut
    {
        public float Duration;
        public float Delay;
        public Ease EaseOut;
    }
}