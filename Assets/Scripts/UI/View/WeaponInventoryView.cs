using UnityEngine;
namespace UI.View
{
    public sealed class WeaponInventoryView : MonoBehaviour
    {
        public WeaponTemplateView WeaponViewPrefab => _weaponViewPrefab;
        public RectTransform Container => _container;
    
        [SerializeField] private RectTransform _container;
        [SerializeField] private WeaponTemplateView _weaponViewPrefab;
    }
}
