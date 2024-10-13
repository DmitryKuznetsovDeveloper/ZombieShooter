using System;
using Code.Tween;
using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Sirenix.OdinInspector;

public sealed class HealthBarView : MonoBehaviour
{
    [SerializeField] private Image _healthIcn;
    [SerializeField] private Image _healthBarFill;
    [SerializeField] private TMP_Text _healthPointLabel;
    [SerializeField] private HealthBarParam _configNormal;
    [SerializeField] private HealthBarParam _configWarning;
    [SerializeField] private HealthBarParam _configDanger;

    private Sequence _sequenceNormal;
    private Sequence _sequenceWarning;
    private Sequence _sequenceDanger;
    private Tween _tweenScaleNormal;
    private Tween _tweenScaleWarning;
    private Tween _tweenScaleDanger;

    public void SetHealthPointLabel(int healthPoint) => _healthPointLabel.text = $"{healthPoint}";
    public void SetHealthBarFill(int currentHealth, int maxHealth)
    {
        var value = (float)currentHealth / maxHealth;
        _healthBarFill.DOFillAmount(value, 1f).SetEase(Ease.Linear);
    }
    public void ShowNormal()
    {
        PauseAllSequences();
        _tweenScaleNormal.Restart();
        PlaySequence(_sequenceNormal);
    }
    
    public void ShowWarning()
    {
        PauseAllSequences();
        _tweenScaleWarning.Restart();
        PlaySequence(_sequenceWarning);
    }
    
    public void ShowDanger()
    {
        PauseAllSequences();
        _tweenScaleDanger.Restart();
        PlaySequence(_sequenceDanger);
    }

    private void Awake() => InitializeSequences();

    private void InitializeSequences()
    {
        _sequenceNormal = CreateSequence(_configNormal);
        _sequenceWarning = CreateSequence(_configWarning);
        _sequenceDanger = CreateSequence(_configDanger);
        
        // Инициализация твинов с бесконечными циклами (для warning и danger)
        _tweenScaleWarning = _healthIcn.rectTransform.DOScale(_configWarning.scaleFactor, _configWarning.TweenParamsOut.Duration).From(1)
            .SetEase(_configWarning.TweenParamsOut.EaseOut)
            .SetLoops(-1)
            .SetRecyclable(true)
            .SetAutoKill(false)
            .SetUpdate(true)
            .Pause();

        _tweenScaleDanger = _healthIcn.rectTransform.DOScale(_configDanger.scaleFactor, _configDanger.TweenParamsOut.Duration).From(1)
            .SetEase(_configDanger.TweenParamsOut.EaseOut)
            .SetLoops(-1)
            .SetRecyclable(true)
            .SetAutoKill(false)
            .SetUpdate(true)
            .Pause();
        
        _tweenScaleNormal = _healthIcn.rectTransform.DOScale(1,_configNormal.TweenParamsOut.Duration)
            .SetEase(_configNormal.TweenParamsOut.EaseOut)
            .SetRecyclable(true)
            .SetAutoKill(false)
            .SetUpdate(true)
            .Pause();
    }

    private Sequence CreateSequence(HealthBarParam config)
    {
        var sequence = DOTween.Sequence();

        sequence.Append(_healthIcn.DOColor(config.HealthIcn, 0));
        sequence.Join(_healthPointLabel.DOColor(config.HealthIcn, 0));
        sequence.Join(_healthBarFill.DOColor(config.HealthBarFill, 0));

        sequence.SetRecyclable(true)
            .SetAutoKill(false)
            .SetUpdate(true)
            .SetEase(config.TweenParamsOut.EaseOut)
            .Pause();

        return sequence;
    }

    private void PlaySequence(Sequence sequence)
    {
        if (sequence == null || sequence.IsPlaying()) return; // Если секвенция уже играет — не перезапускаем
        sequence.OnComplete(()=> sequence.Pause()).Restart();
    }
    private void OnDisable() => PauseAllSequences();

    private void PauseAllSequences()
    {
        _sequenceNormal?.Pause();
        _sequenceWarning?.Pause();
        _sequenceDanger?.Pause();
        _tweenScaleNormal?.Pause();
        _tweenScaleWarning?.Pause();
        _tweenScaleDanger?.Pause();
    }

    private void OnDestroy() => KillAllSequences();

    private void KillAllSequences()
    {
        _sequenceNormal?.Kill();
        _sequenceWarning?.Kill();
        _sequenceDanger?.Kill();
        _tweenScaleNormal?.Kill();
        _tweenScaleWarning?.Kill();
        _tweenScaleDanger?.Kill();
    }
}

[Serializable]
public class HealthBarParam
{
    public Color HealthIcn;
    public Color HealthBarFill;
    public float scaleFactor;
    public TweenParamsOut TweenParamsOut;
}