using System.Collections.Generic;
using DG.Tweening;
using Game.Data;
using TMPro;
using UI;
using UnityEngine;
using UnityEngine.UI;

public sealed class WeaponTemplateView : AnimationStatesBaseUIElement<WeaponTemplateView.WeaponViewAnimationState,WeaponInfoBackgroundSettings,WeaponInfoBackgroundConfig>
{
    [SerializeField] private CanvasGroup _mainCanvasGroup;
    [SerializeField] private Image _bgMain;
    [SerializeField] private Image _bgGradient;
    [SerializeField] private Image _frameSelected;
    [SerializeField] private Image _imageWeapon;
    [SerializeField] private TMP_Text _weaponAmmoLabel;
    
    public enum WeaponViewAnimationState{Normal,Selected};



    public void SetWeaponAmmoLabel(int clip,string richText,int totalAmmo) => _weaponAmmoLabel.text = $"{clip} {richText} {totalAmmo}";

    public void SetSpriteWeapon(Sprite sprite) => _imageWeapon.sprite = sprite;

    protected override void InitializeSequences()
    {
        AnimationSequences = new Dictionary<WeaponViewAnimationState, Sequence>()
        {
            {WeaponViewAnimationState.Normal,CreateSequence(_config.NormalState)},
            {WeaponViewAnimationState.Selected,CreateSequence(_config.SelectedState)},
        };
    }
    
    protected override Sequence CreateSequence(WeaponInfoBackgroundSettings config)
    {
        var sequence = DOTween.Sequence();
        sequence.Append(_mainCanvasGroup.DOFade(config.MainFade, config.Params.Duration));
        sequence.Join(_imageWeapon.rectTransform.DOScale(config.scaleFactorWeapon, config.Params.Duration).From(1));
        sequence.Join(_bgMain.DOColor(config.BgMainColor, config.Params.Duration));
        sequence.Join(_frameSelected.DOColor(config.FrameSelectedColor, config.Params.Duration));
        sequence.Join(_bgGradient.DOColor(config.BgGradientColor, config.Params.Duration));
        sequence.Join(_frameSelected.DOFade(config.FrameSelectedFade, config.Params.Duration));
        sequence.Join(_bgGradient.rectTransform.DOSizeDelta(config.SizeDeltaGradientTarget, config.Params.Duration).From(config.SizeDeltaGradientFrom));
        sequence.
            SetRecyclable(true)
            .SetAutoKill(false)
            .SetUpdate(true)
            .SetEase(config.Params.EaseOut)
            .Pause();
        return sequence;
    }
    protected override WeaponViewAnimationState GetDefaultState() => WeaponViewAnimationState.Normal;
}


