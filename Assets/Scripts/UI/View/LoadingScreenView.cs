using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace SkyCombat.Scripts.UI
{
    public class LoadingScreenView : MonoBehaviour
    {
        [Header("Slider Settings")]
        [SerializeField] private Slider _slider;
        [SerializeField] private TMP_Text _loadingLabel;
        [SerializeField] private RectTransform _decorMove;
        [SerializeField] private CanvasGroup _gradientCanvasGroup;
        [SerializeField] private RectTransform _gradientRectTransform;
        [SerializeField] private float _delayGradientSlider;
        [SerializeField] private float _durationSlider;
        [SerializeField] private Ease _easeSlider;

        private readonly Vector2 _startGradientAnchorsMin = new Vector2(0, 0);
        private readonly Vector2 _endGradientAnchorsMin = new Vector2(1, 0);
        private readonly Vector2 _startGradientAnchorsMax = new Vector2(0, 1);
        private readonly Vector2 _endGradientAnchorsMax = new Vector2(1, 1);
        private readonly Vector2 _startDecorMove = new Vector2(0f, 0.5f);
        private readonly Vector2 _endDecorMove = new Vector2(1, 0.5f);
        private Sequence _sequenceLoadingSlider;
        private void Awake()
        {
            _sequenceLoadingSlider = DOTween.Sequence();
            _sequenceLoadingSlider.Append(_gradientCanvasGroup.DOFade(1, _durationSlider).From(0f));
            _sequenceLoadingSlider.Join(_loadingLabel.DOFade(1f, _durationSlider).From(0f));
            _sequenceLoadingSlider.Join(_gradientRectTransform.DOAnchorMax(_endGradientAnchorsMax, _durationSlider).From(_startGradientAnchorsMax));
            _sequenceLoadingSlider.Append(_gradientRectTransform.DOAnchorMin(_endGradientAnchorsMin, _durationSlider).From(_startGradientAnchorsMin).SetDelay(_delayGradientSlider));
            _sequenceLoadingSlider.Join(_decorMove.DOAnchorMin(_endDecorMove, _durationSlider).From(_startDecorMove));
            _sequenceLoadingSlider.Join(_decorMove.DOAnchorMax(_endDecorMove, _durationSlider).From(_startDecorMove));
            _sequenceLoadingSlider.Join(_decorMove.DOPivot(_endDecorMove, _durationSlider).From(_startDecorMove));
            _sequenceLoadingSlider.Join(_loadingLabel.DOFade(0f, _durationSlider).SetDelay(_delayGradientSlider));
            _sequenceLoadingSlider.Join(_gradientCanvasGroup.DOFade(0, _durationSlider));
            _sequenceLoadingSlider
                .SetEase(_easeSlider)
                .SetLoops(-1, LoopType.Restart)
                .SetAutoKill(false)
                .SetUpdate(true)
                .Pause();
        }
        public void SetSlider(float value) => _slider.DOValue(value, 1f);

        private void PauseSliderAnimation() => _sequenceLoadingSlider.Pause();
        private void ShowLoadingScreen() => _sequenceLoadingSlider.Restart();
        private void OnEnable() => ShowLoadingScreen();
        private void OnDisable() => _sequenceLoadingSlider.OnComplete(PauseSliderAnimation).Rewind();
        private void OnDestroy() => _sequenceLoadingSlider.Kill();
    }
}