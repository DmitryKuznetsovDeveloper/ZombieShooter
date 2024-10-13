using Code.Tween;
using DG.Tweening;
using UnityEngine;

namespace UI.View
{
    public class BasePopupView : MonoBehaviour
    {
        public ButtonDefaultView CloseButton => _closeButton;

        [SerializeField] private CanvasGroup _mainCanvasGroup;
        [SerializeField] private RectTransform _popupBase;
        [SerializeField] private ButtonDefaultView _closeButton;
        [SerializeField] private TweenParamsOutIn _tweenParams;

        private Sequence _sequenceShow;
        private Sequence _sequenceHide;
        private bool _isVisible;

        private void Awake() => InitializePopup();

        public void Show()
        {
            if (_isVisible) return; 
            _sequenceHide?.Complete();
            _mainCanvasGroup.interactable = true;
            _mainCanvasGroup.blocksRaycasts = true;
            _sequenceShow.OnComplete(PauseShow)?.Restart();
            _isVisible = true;
        }

        public void Hide()
        {
            if (!_isVisible) return;
            _sequenceShow?.Complete();
            _mainCanvasGroup.interactable = false;
            _mainCanvasGroup.blocksRaycasts = false;
            _sequenceHide.OnComplete(PauseHide)?.Restart();
            _isVisible = false;
        }

        private void InitializePopup()
        {
            _mainCanvasGroup.interactable = false;
            _mainCanvasGroup.blocksRaycasts = false;
            CreateShowSequence();
            CreateHideSequence();
        }

        private void CreateShowSequence()
        {
            _sequenceShow = DOTween.Sequence();
            _sequenceShow.Append(_mainCanvasGroup.DOFade(1f, 0).From(0));
            _sequenceShow.Join(_popupBase.DOScale(1, _tweenParams.Duration).From(0.5f));
            _sequenceShow.SetRecyclable(true).SetEase(_tweenParams.EaseOut).SetAutoKill(false).SetUpdate(true).Pause();
        }

        private void CreateHideSequence()
        {
            _sequenceHide = DOTween.Sequence();
            _sequenceHide.Append(_mainCanvasGroup.DOFade(0f, _tweenParams.Duration));
            _sequenceHide.Join(_popupBase.DOScale(0, _tweenParams.Duration));
            _sequenceHide.SetRecyclable(true).SetEase(_tweenParams.EaseIn).SetAutoKill(false).SetUpdate(true).Pause();
        }
        private void PauseShow() => _sequenceShow.Pause();
        private void PauseHide() => _sequenceHide.Pause();

        private void OnDisable()
        {
            PauseShow();
            PauseHide();
        }

        private void OnDestroy()
        {
            _sequenceHide?.Kill();
            _sequenceShow?.Kill();
        }
    }
}