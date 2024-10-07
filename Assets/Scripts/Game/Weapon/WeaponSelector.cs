using UnityEngine;

namespace Game.Weapon
{
    public sealed class WeaponSelector
    {
        public BaseWeapon CurrentBaseWeapon => _currentBaseWeapon;
        private int _currentWeaponIndex;
        private BaseWeapon _currentBaseWeapon;
        private BaseWeapon[] _weapons;
        
        public void Init(BaseWeapon[] weapons)
        {
            if (weapons == null || weapons.Length == 0)
            {
                Debug.LogError("Weapon array is empty or null");
                return;
            }

            _weapons = weapons;
            _currentWeaponIndex = 0;
            _currentBaseWeapon = _weapons[_currentWeaponIndex];

            // Делаем активным только первое оружие, все остальные скрываем
            for (var i = 0; i < _weapons.Length; i++)
                _weapons[i].gameObject.SetActive(i == _currentWeaponIndex);
        }
        
        public void Next()
        {
            if (!_currentBaseWeapon.IsReload)
            {
                _currentWeaponIndex = (_currentWeaponIndex + 1) % _weapons.Length; // Обрабатываем переполнение
                SelectWeapon();
            }
        }
        
        public void Previous()
        {
            if (!_currentBaseWeapon.IsReload)
            {
                _currentWeaponIndex = (_currentWeaponIndex - 1 + _weapons.Length) % _weapons.Length; // Обрабатываем отрицательные значения индекса
                SelectWeapon();
            }
        }
        
        public void GetWeapon() => SelectWeapon();
        
        private void SelectWeapon()
        {
            if (!_currentBaseWeapon.Equals(null))
                _currentBaseWeapon.gameObject.SetActive(false);
                
            _currentBaseWeapon = _weapons[_currentWeaponIndex];
            _currentBaseWeapon.gameObject.SetActive(true);
        }
    }
}