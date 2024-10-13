using System;
using UnityEngine;
using Sirenix.OdinInspector;

[CreateAssetMenu(fileName = "NewWeaponConfig", menuName = "Weapon/Config")]
public sealed class WeaponConfig : ScriptableObject
{
    [Title("Weapon General Settings")] // Основные настройки оружия
    [VerticalGroup("General", PaddingTop = 10)] // Логическая группировка
    [PreviewField(100, ObjectFieldAlignment.Left)] // Отображение иконки с увеличением
    [HideLabel] // Скрываем стандартную подпись для PreviewField
    [SerializeField] private Sprite _weaponIcon; // Иконка оружия

    [VerticalGroup("General")] // Оружие типа
    [LabelText("Weapon Type")]
    [SerializeField] private WeaponType _weaponType; 
    
    [VerticalGroup("General")] // Оружие типа
    [LabelText("Weapon Rich text ammo")]
    [SerializeField] private string _richTexttAmmo; // Тип оружия

    [TabGroup("Ammo & Performance", "Ammo")] // Вкладка "Боеприпасы"
    [LabelText("Total Ammo")]
    [Tooltip("Общее количество патронов")]
    [SerializeField] private int _totalAmmo = 120; // Общее количество патронов

    [TabGroup("Ammo & Performance", "Ammo")]
    [LabelText("Magazine Capacity")]
    [Tooltip("Максимальное количество патронов в обойме")]
    [SerializeField] private int _magazineCapacity = 30; // Патроны в обойме

    [TabGroup("Ammo & Performance", "Performance")]
    [LabelText("Damage")]
    [Tooltip("Урон")]
    [SerializeField] private int _damage = 30; // Урон

    [TabGroup("Ammo & Performance", "Performance")] // Вкладка "Производительность"
    [LabelText("Range")]
    [Tooltip("Дальность стрельбы")]
    [SerializeField] private float _range = 100f; // Дальность стрельбы

    [TabGroup("Ammo & Performance", "Performance")]
    [LabelText("Fire Rate")]
    [Tooltip("Скорострельность (выстрелов в секунду)")]
    [SerializeField] private float _fireRate = 10f; // Скорострельность

    [TabGroup("Ammo & Performance", "Performance")]
    [LabelText("Impact Force")]
    [Tooltip("Сила воздействия при попадании")]
    [SerializeField] private float _impactForce = 50f; // Сила при попадании

    [TabGroup("Visual & Effects", "Effects")] // Вкладка "Эффекты"
    [LabelText("Muzzle Flash Effect")]
    [Tooltip("Эффект вспышки выстрела")]
    [AssetsOnly]
    [InlineEditor(InlineEditorObjectFieldModes.Boxed)] // Встроенное превью
    [SerializeField] private ParticleSystem _muzzleFlashEffect; // Эффект вспышки

    [TabGroup("Visual & Effects", "Effects")]
    [LabelText("Shell Ejection Effect")]
    [Tooltip("Эффект вылета гильз")]
    [AssetsOnly]
    [InlineEditor(InlineEditorObjectFieldModes.Boxed)]
    [SerializeField] private ParticleSystem _shellEjectionEffect; // Эффект гильз

    [TabGroup("Visual & Effects", "Effects")]
    [LabelText("Hit Effect (Enemy)")]
    [Tooltip("Эффект при попадании во врага")]
    [AssetsOnly]
    [InlineEditor(InlineEditorObjectFieldModes.Boxed)]
    [SerializeField] private ParticleSystem _bloodEffect; // Эффект при попадании во врага

    [TabGroup("Visual & Effects", "Effects")]
    [LabelText("Hit Effect (Surface)")]
    [Tooltip("Эффект при попадании по поверхности")]
    [AssetsOnly]
    [InlineEditor(InlineEditorObjectFieldModes.Boxed)]
    [SerializeField] private ParticleSystem _surfaceImpactEffect; // Эффект попадания

    [TabGroup("Visual & Effects", "Effects")]
    [LabelText("Bullet Hole Decal")]
    [Tooltip("Декаль дыры от пули")]
    [AssetsOnly]
    [InlineEditor(InlineEditorObjectFieldModes.Boxed)]
    [SerializeField] private ParticleSystem _bulletHoleDecal; // Декаль от пули

    // Публичные геттеры
    public WeaponType WeaponType => _weaponType;
    public Sprite WeaponIcon => _weaponIcon;
    public int TotalAmmo => _totalAmmo;
    public int MagazineCapacity => _magazineCapacity;
    public int Damage => _damage;
    public float Range => _range;
    public float FireRate => _fireRate;
    public float ImpactForce => _impactForce;
    public ParticleSystem MuzzleFlashEffect => _muzzleFlashEffect;
    public ParticleSystem ShellEjectionEffect => _shellEjectionEffect;
    public ParticleSystem BloodEffect => _bloodEffect;
    public ParticleSystem SurfaceImpactEffect => _surfaceImpactEffect;
    public ParticleSystem BulletHoleDecal => _bulletHoleDecal;
}

[Serializable]
public enum WeaponType
{
    Pistol,
    Scar,
    Shotgun,
}