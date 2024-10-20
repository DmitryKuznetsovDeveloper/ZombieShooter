using System;
using Sirenix.OdinInspector;
using UnityEngine;
namespace Game.Components
{
    public sealed class HealthComponent : MonoBehaviour
    {
        public event Action<int> OnChangeHealth;
        public event Action<int> OnTakeDamage;
        public event Action OnDeath;
        
        [SerializeField, Range(0,100)] private int _maxHitPoints = 100;
        [SerializeField, Range(0,100)] private int _health = 50;

        public int MaxHitPoints => _maxHitPoints;
        public int CurrentHealthPoints => _health;
        
        public void TakeDamage(int damage)
        {
            _health = Math.Max(0, _health - damage);
            if (_health <= 0) OnDeath?.Invoke();
            else OnTakeDamage?.Invoke(damage);
            OnChangeHealth?.Invoke(_health);
        }
        
        public void RestoreHitPoints(int healthPoints)
        {
            _health = healthPoints > 0 ? Mathf.Min(_health + healthPoints, _maxHitPoints) : _health;
            OnChangeHealth?.Invoke(healthPoints);   
        }
        private void Awake() =>
            OnChangeHealth?.Invoke(_health);
    }
}