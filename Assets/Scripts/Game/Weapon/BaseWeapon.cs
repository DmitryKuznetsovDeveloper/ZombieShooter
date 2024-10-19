using System;
using System.Threading;
using Cinemachine;
using Cysharp.Threading.Tasks;
using Game.Animations;
using Game.Components;
using Game.Pool;
using Sirenix.OdinInspector;
using UnityEngine;
using Zenject;

namespace Game.Weapon
{
    public abstract class BaseWeapon : MonoBehaviour
    {
        public event Action<int,string,int> OnChangeAmmo;
        public bool IsReload => _isReload;
        public WeaponType WeaponType => _weaponType;
        public WeaponConfig WeaponConfig => _weaponConfig;
        public CinemachineVirtualCamera BaseCamera => _baseCamera;
        
        [SerializeField,ReadOnly] private WeaponType _weaponType;
        [SerializeField] private Transform _barrelTransform;
        [SerializeField,InlineEditor] protected WeaponConfig _weaponConfig;
        [SerializeField] protected Transform _muzzleFlashContainer;
        [SerializeField] protected Transform _bloodContainer;
        [SerializeField] protected Transform _impactContainer;
        [SerializeField] protected Transform _shellContainer;
        [SerializeField] protected Transform _holeContainer;
        [SerializeField] protected CinemachineVirtualCamera _baseCamera;
        [SerializeField] protected Animator _animator;
        [SerializeField] protected LayerMask _characterLayerMask;

        protected int _currentClip;
        protected int _totalAmmo;
        
        private WeaponBaseAnimations _weaponBaseAnimations;
        private ObjectPool<ParticleSystem> _muzzleFlashPool; // Пул для вспышек выстрела
        private ObjectPool<ParticleSystem> _bloodEffectPool; // Пул для эффектов крови
        private ObjectPool<ParticleSystem> _impactEffectPool; // Пул для эффектов попаданий
        private ObjectPool<ParticleSystem> _shellEffectPool; // Пул для эффектов попаданий
        private ObjectPool<ParticleSystem> _holePool; // Пул для эффектов попаданий// Камера игрока
        private float _nextTimeToFire;
        private bool _isReload;
        private CancellationTokenSource _cancellationTokenSource;
        
        [Inject]
        public void Construct( WeaponBaseAnimations weaponBaseAnimations) => _weaponBaseAnimations = weaponBaseAnimations;

        protected virtual void Awake() => InitializeWeapon();
        protected virtual void InitializeWeapon()
        {

            _weaponType = _weaponConfig.WeaponType;
            _currentClip = _weaponConfig.MagazineCapacity;
            _totalAmmo = _weaponConfig.TotalAmmo;
            _muzzleFlashPool = new ObjectPool<ParticleSystem>(_weaponConfig.MuzzleFlashEffect, _weaponConfig.MagazineCapacity / 2, _muzzleFlashContainer);
            _bloodEffectPool = new ObjectPool<ParticleSystem>(_weaponConfig.BloodEffect, _weaponConfig.MagazineCapacity / 2, _bloodContainer);
            _impactEffectPool = new ObjectPool<ParticleSystem>(_weaponConfig.SurfaceImpactEffect, _weaponConfig.MagazineCapacity / 2, _impactContainer);
            _shellEffectPool = new ObjectPool<ParticleSystem>(_weaponConfig.ShellEjectionEffect, _weaponConfig.MagazineCapacity / 2, _shellContainer);
            _holePool = new ObjectPool<ParticleSystem>(_weaponConfig.BulletHoleDecal, _weaponConfig.MagazineCapacity, _holeContainer);
        }
        public void Shoot()
        {
            if (Time.time >= _nextTimeToFire && !_isReload && _currentClip > 0)
            {
                ShootProcess();
                _nextTimeToFire = Time.time + 1f / _weaponConfig.FireRate; // Обновляем время следующего выстрела
            }
        }

        public async UniTask Recharge()
        {
            if (_currentClip >= _weaponConfig.MagazineCapacity || _totalAmmo == 0)
                return;

            _isReload = true;
            await RechargeProcess();
            await _weaponBaseAnimations.WaitForRechargeAnimationEnd(_animator, _cancellationTokenSource.Token);
            OnChangeAmmo?.Invoke(_currentClip,WeaponConfig.RichTextAmmo,_totalAmmo);
            _isReload = false;
        }
        protected abstract UniTask RechargeProcess();

        protected virtual void ShootProcess()
        {
            _weaponBaseAnimations.ShowShoot(_animator);
            _currentClip--;
            OnChangeAmmo?.Invoke(_currentClip,WeaponConfig.RichTextAmmo,_totalAmmo);
            
            Vector3 rayOrigin = _barrelTransform.position;
            Vector3 rayDirection = _barrelTransform.forward; 

            PlayEffectParent(_muzzleFlashPool, _muzzleFlashContainer);
            
            if (Physics.Raycast(rayOrigin, rayDirection,out var hit, _weaponConfig.Range, ~_characterLayerMask))
                HandleHit(hit);
            
            PlayEffectParent(_shellEffectPool, _shellContainer);
        }
        protected virtual void HandleHit(RaycastHit hit)
        {

            if (hit.transform.root.TryGetComponent(out HealthComponent healthComponent))
            {
                healthComponent.TakeDamage(_weaponConfig.Damage);
                PlayEffectHit(_bloodEffectPool, hit);
            }
            else
            {
                PlayEffectHit(_impactEffectPool, hit);
                PlayEffectAndAssignParent(_holePool, hit);
            }

            if (hit.transform.TryGetComponent(out Rigidbody rb))
            {
                rb.AddForce(-hit.normal * _weaponConfig.ImpactForce);
            }
        }

        private void PlayEffectHit(ObjectPool<ParticleSystem> pool, RaycastHit hit)
        {
            var poolObject = pool.GetObject();
            var poolObjectTransform = poolObject.transform;
            poolObjectTransform.position = hit.point;
            poolObjectTransform.rotation = Quaternion.LookRotation(hit.normal);
            poolObject.Play(true);
            ReturnEffectToPoolAfterTime(pool, poolObject).Forget();
        }

        private void PlayEffectParent(ObjectPool<ParticleSystem> pool, Transform parent)
        {
            var poolObject = pool.GetObject();
            var poolObjectTransform = poolObject.transform;
            poolObjectTransform.position = parent.position;
            poolObjectTransform.rotation = parent.rotation;
            poolObject.Play(true);
            ReturnEffectToPoolAfterTime(pool, poolObject).Forget();
        }

        private void PlayEffectAndAssignParent(ObjectPool<ParticleSystem> pool, RaycastHit hit)
        {
            var poolObject = pool.GetObject();
            var poolObjectTransform = poolObject.transform;
            poolObjectTransform.SetParent(hit.transform);
            poolObjectTransform.position = hit.point;
            poolObjectTransform.rotation = Quaternion.LookRotation(hit.normal);
            poolObject.Play(true);
            ReturnEffectToPoolAfterTime(pool, poolObject).Forget();
        }

        private async UniTask ReturnEffectToPoolAfterTime(ObjectPool<ParticleSystem> pool, ParticleSystem effect)
        {
            // Ожидаем, пока эффект не завершит воспроизведение
            while (effect.isPlaying)
                await UniTask.Yield(PlayerLoopTiming.Update, _cancellationTokenSource.Token); // Ждем следующий кадр

            effect.transform.SetParent(pool.Parent);
            pool.ReturnObject(effect);
        }
        protected void OnEnable() => _cancellationTokenSource = new CancellationTokenSource();
        protected void OnDisable() => _cancellationTokenSource.Cancel();
        protected void OnDestroy() => _cancellationTokenSource.Cancel();
        private void OnDrawGizmos()
        {
            // Если у вас есть ссылка на ствол оружия
            if (_barrelTransform != null)
            {
                // Устанавливаем цвет Gizmos
                Gizmos.color = Color.red; // Цвет луча (красный)

                // Рисуем линию от ствола до направления луча
                Gizmos.DrawRay(_barrelTransform.position, _barrelTransform.forward * _weaponConfig.Range);
            }
        }
    }
}