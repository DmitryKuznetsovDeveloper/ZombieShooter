using Code.Tween;
using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

public sealed class WeaponTemplateView : MonoBehaviour
{
    [SerializeField] private CanvasGroup _mainCanvasGroup;
    [SerializeField] private Image _bgMain;
    [SerializeField] private Image _bgGradient;
    [SerializeField] private Image _frameSelected;
    [FormerlySerializedAs("_backgroundConfig")]
    [SerializeField] private WeaponInfoBackgroundConfig _normalState;
    [SerializeField] private WeaponInfoBackgroundConfig _selectedState;
    [Space(10)]
    [SerializeField] private Image _imageWeapon;
    [SerializeField] private TMP_Text _weaponAmmoLabel;

    private Sequence _sequenceNormal;
    private Sequence _sequenceSelected;

    public void ShowNormal()
    {
        _sequenceSelected.OnComplete(PauseSelected)?.Complete();
        _sequenceNormal.OnComplete(PauseNormal)?.Restart();
    }

    public void ShowSelected()
    {
        _sequenceSelected.OnComplete(PauseSelected)?.Restart();
    }

    public void SetWeaponAmmoLabel(int clip, int totalAmmo, string richText) => _weaponAmmoLabel.text = $"{clip} {richText} {totalAmmo}";

    public void SetSpriteWeapon(Sprite sprite) => _imageWeapon.sprite = sprite;

    public void InitSequence()
    {
        _sequenceSelected = CreateSequenceState(_selectedState);
        _sequenceNormal = CreateSequenceState(_normalState);
    }
    private Sequence CreateSequenceState(WeaponInfoBackgroundConfig config)
    {
        var sequence = DOTween.Sequence();
        sequence.Append(_mainCanvasGroup.DOFade(config.MainFade, config.TweenParamsOut.Duration));
        sequence.Join(_imageWeapon.rectTransform.DOScale(config.scaleFactorWeapon, config.TweenParamsOut.Duration).From(1));
        sequence.Join(_bgMain.DOColor(config.BgMainColor, config.TweenParamsOut.Duration));
        sequence.Join(_frameSelected.DOColor(config.FrameSelectedColor, config.TweenParamsOut.Duration));
        sequence.Join(_bgGradient.DOColor(config.BgGradientColor, config.TweenParamsOut.Duration));
        sequence.Join(_frameSelected.DOFade(config.FrameSelectedFade, config.TweenParamsOut.Duration));
        sequence.Join(_bgGradient.rectTransform.DOSizeDelta(config.SizeDeltaGradientTarget, config.TweenParamsOut.Duration).From(config.SizeDeltaGradientFrom));
        sequence.
            SetRecyclable(true)
            .SetAutoKill(false)
            .SetUpdate(true)
            .SetEase(config.TweenParamsOut.EaseOut)
            .Pause();
        return sequence;
    }

    private void PauseNormal() => _sequenceNormal.Pause();
    private void PauseSelected() => _sequenceSelected.Pause();

    private void OnEnable() => ShowNormal();
    private void OnDisable()
    {
        PauseNormal();
        PauseSelected();
    }

    private void OnDestroy()
    {
        _sequenceNormal?.Kill();
        _sequenceSelected?.Kill();
    }
}

[System.Serializable]
public class WeaponInfoBackgroundConfig
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
    public TweenParamsOut TweenParamsOut;

}


