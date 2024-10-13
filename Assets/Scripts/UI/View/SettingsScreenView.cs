using Code.Tween;
using DG.Tweening;
using UnityEngine;

namespace UI.View
{
    public sealed class SettingsScreenView : MonoBehaviour
    {
        public ButtonDefaultView CloseButton => _closeButton;
        
        [SerializeField] private CanvasGroup _mainCanvasGroup;
        [SerializeField] private RectTransform _popupBase;
        [SerializeField] private ButtonDefaultView _closeButton;
        [SerializeField] private TweenParamsOutIn _tweenParams;

        private Sequence _sequenceShow;

        
        public void Show()
        {
            _mainCanvasGroup.interactable = true;
            _mainCanvasGroup.blocksRaycasts = true;
            _sequenceShow.OnComplete(PauseShow).SetEase(_tweenParams.EaseOut).Restart();
        }
        public void Hide()
        {
            _mainCanvasGroup.interactable = false;
            _mainCanvasGroup.blocksRaycasts = false;
            _sequenceShow.OnComplete(PauseShow).SetEase(_tweenParams.EaseIn).PlayBackwards();
        }
        private void Awake()
        {
            _mainCanvasGroup.interactable = false;
            _mainCanvasGroup.blocksRaycasts = false;
            _sequenceShow = DOTween.Sequence();
            _sequenceShow.Append(_mainCanvasGroup.DOFade(1f, _tweenParams.Duration).From(0));
            _sequenceShow.Join(_popupBase.DOScale(1, _tweenParams.Duration).From(0.5f));
            _sequenceShow
                .SetRecyclable(true)
                .SetAutoKill(false)
                .SetUpdate(true)
                .Pause();
        }
        private void PauseShow() => _sequenceShow.Pause();
        private void OnDisable() => PauseShow();
        private void OnDestroy() => _sequenceShow.Kill();



    }
}