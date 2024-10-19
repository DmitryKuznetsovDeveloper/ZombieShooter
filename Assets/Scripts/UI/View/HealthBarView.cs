using System.Collections.Generic;
using DG.Tweening;
using Game.Data;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Sequence = DG.Tweening.Sequence;

namespace UI.View
{
    public sealed class HealthBarView : AnimationStatesBaseUIElement<HealthBarView.HealthBarAnimationState,HealthBarSettings,HealthBarConfig>
    {
        [SerializeField] private Image _healthIcn;
        [SerializeField] private Image _healthBarFill;
        [SerializeField] private TMP_Text _healthPointLabel;
    
        public enum HealthBarAnimationState{Normal,Warning,Danger};

        public void SetHealthPointLabel(int healthPoint) => _healthPointLabel.text = $"{healthPoint}";
        public void SetHealthBarFill(int currentHealth)
        {
            var value = (float)currentHealth / 100;
            _healthBarFill.DOFillAmount(value, 1f).SetEase(Ease.Linear);
        }

        protected override void InitializeSequences()
        {
            AnimationSequences = new Dictionary<HealthBarAnimationState, Sequence>()
            {
                { HealthBarAnimationState.Normal, CreateSequence(_config.NormalStae) },
                { HealthBarAnimationState.Warning, CreateSequence(_config.WarninglStae) },
                { HealthBarAnimationState.Danger, CreateSequence(_config.DangerStae) },
            };
        }

        protected override Sequence CreateSequence(HealthBarSettings config)
        {
            var sequence = DOTween.Sequence();

            sequence.Append(_healthIcn.DOColor(config.HealthIcn, 0));
            sequence.Join(_healthPointLabel.DOColor(config.HealthIcn, 0));
            sequence.Join(_healthBarFill.DOColor(config.HealthBarFill, 0));
            sequence.AppendCallback(() =>
            {
                _healthIcn.rectTransform.DOScale(config.ScaleFactor, config.Params.Duration)
                    .SetLink(_healthIcn.gameObject)
                    .SetLoops(-1, LoopType.Yoyo)
                    .SetEase(config.Params.EaseOut)
                    .SetUpdate(true);
            });
            sequence.SetRecyclable(true)
                .SetAutoKill(false)
                .SetUpdate(true)
                .SetEase(config.Params.EaseOut)
                .Pause();
            return sequence;
        }
        protected override HealthBarAnimationState GetDefaultState() => HealthBarAnimationState.Normal;
    }
}