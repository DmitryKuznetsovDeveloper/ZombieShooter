using System;
using UnityEngine;
namespace Game.Components
{
    public sealed class HealthComponent : MonoBehaviour
    {
        public event Action<int> OnChangeHealth;
        public event Action<int> OnTakeDamage;
        public event Action OnDeath;
        
        [SerializeField, Min(0)] private int _maxHitPoints = 100;
        [SerializeField, Min(0)] private int _health = 50;
        
        public void TakeDamage(int damage)
        {
            _health = Math.Max(0, _health - damage);
            if (_health <= 0) OnDeath?.Invoke();
            else OnTakeDamage?.Invoke(damage);
            OnChangeHealth?.Invoke(_health);
        }

        public void RestoreHitPoints(int healthPoints)
        {
            _health = Mathf.Min(_health + healthPoints, _maxHitPoints);
            OnChangeHealth?.Invoke(healthPoints);
        }
    }
}