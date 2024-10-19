using System.Collections.Generic;
using DG.Tweening;
using Game.Data;
using UnityEngine;

namespace UI.View
{
    public class BasePopupView : AnimationStatesBaseUIElement<BasePopupView.BasePopupState, BasePopupSettings, BasePopupConfig>, IPopup
    {
        public ButtonDefaultView CloseButton => _closeButton;

        [SerializeField] private CanvasGroup _mainCanvasGroup;
        [SerializeField] private RectTransform _popupBase;
        [SerializeField] private ButtonDefaultView _closeButton;

        public enum BasePopupState { Show, Hide }

        protected override void InitializeSequences()
        {
            AnimationSequences = new Dictionary<BasePopupState, Sequence>()
            {
                { BasePopupState.Show, CreateSequence(_config.ShowState) },
                { BasePopupState.Hide, CreateSequence(_config.HideState) },
            };
            Hide();
        }
        
        protected override Sequence CreateSequence(BasePopupSettings stateConfig)
        {
            var sequence = DOTween.Sequence();
            sequence.Append(_mainCanvasGroup.DOFade(stateConfig.MainFade, stateConfig.Params.Duration));
            sequence.Join(_popupBase.DOScale(stateConfig.TargetScale, stateConfig.Params.Duration).From(stateConfig.StartScale));
            sequence.SetRecyclable(true)
                .SetAutoKill(false)
                .SetUpdate(true)
                .SetEase(stateConfig.Params.EaseOut)
                .Pause();
            return sequence;
        }
        protected override BasePopupState GetDefaultState() => BasePopupState.Hide;

        public void Show()
        {
            SwitchCanvasGroupEnable(true);
            PlayAnimation(BasePopupState.Show);
        }
        public void Hide()
        {
            SwitchCanvasGroupEnable(false);
            PlayAnimation(BasePopupState.Hide);
        }
        private void SwitchCanvasGroupEnable(bool enable)
        {
            _mainCanvasGroup.interactable = enable;
            _mainCanvasGroup.blocksRaycasts = enable;
        }

    }
}