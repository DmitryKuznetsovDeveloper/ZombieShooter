using Code.Tween;
using DG.Tweening;
using TMPro;
using UI.UIExtension;
using UnityEngine;
using UnityEngine.UI;

namespace UI.View
{
    public sealed class ButtonDefaultView : MonoBehaviour
    {
        public Sequence SequenceHover => _sequenceHover;
        public TweenParamsOutIn TweenParamsHover => _tweenParamsHover;
        public Sequence SequenceClick => _sequenceClick;
        public TweenParamsOut TweenParamsClick => _tweenParamsClick;
        public Button Button => _button;
        
        [SerializeField] private Button _button;
        [Header("Hover Settings")]
        [SerializeField] private Image _shadow;
        [SerializeField] private Color _shadowColor;
        [SerializeField] private TMP_Text _buttonLabel;
        [SerializeField] private Color _labelColor;
        [SerializeField] private TweenParamsOutIn _tweenParamsHover;
        
        [Header("Click Settings")]
        [SerializeField] private RectTransform _root;
        [SerializeField] private float _scaleFactor;
        [SerializeField] private TweenParamsOut _tweenParamsClick;

        private Sequence _sequenceHover;
        private Sequence _sequenceClick;
        private void Awake()
        {
            _sequenceHover = DOTween.Sequence();
            _sequenceHover.Append(_shadow.DOColor(_shadowColor, _tweenParamsHover.Duration));
            _sequenceHover.Join(_buttonLabel.DOColor(_labelColor, _tweenParamsHover.Duration));
            _sequenceHover
                .SetRecyclable(true)
                .SetAutoKill(false)
                .SetUpdate(true)
                .Pause();

            _sequenceClick = DOTween.Sequence();
            _sequenceClick.Append(_root.DOScale(_scaleFactor, _tweenParamsClick.Duration).SetLoops(2,LoopType.Yoyo).From(1));
            _sequenceClick
                .SetRecyclable(true)
                .SetAutoKill(false)
                .SetUpdate(true)
                .Pause();
        }
        
        private void OnDisable()
        {
            _sequenceHover?.Pause();
            _sequenceClick?.Pause();
        }

        private void OnDestroy()
        {
            _button.UnsubscribeSequence(_sequenceHover,_sequenceClick);
            _button.UnsubscribeAll();
        }
    }
}
