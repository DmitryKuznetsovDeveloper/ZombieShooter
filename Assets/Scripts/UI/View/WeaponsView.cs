using UnityEngine;

public sealed class WeaponsView : MonoBehaviour
{
    public WeaponTemplateView WeaponViewPrefab => _weaponViewPrefab;
    public RectTransform Container => _container;
    
    [SerializeField] private RectTransform _container;
    [SerializeField] private WeaponTemplateView _weaponViewPrefab;
    
    
}
