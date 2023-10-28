using NaughtyAttributes;
using Ren.Attachment;
using System;
using System.Collections;
using UnityEngine;

namespace Ren.Weapon
{
    [RequireComponent(typeof(SpriteRenderer))]
    public class Weapon : MonoBehaviour, IInteractable, IUseable
    {
        public WeaponData Data
        {
            get { return _data; }
            set { _data = value; }
        }

        public Transform FirePointTransform
        { 
            get { return _firePointTransform; } 
        }

        public event Action<Weapon> OnWeaponFire
        {
            add 
            { 
                if(_onWeaponFiredAtRate == null)
                {
                    _onWeaponFiredAtRate += value;
                }
                else
                {
                    _onWeaponFiredAtRate = null;
                    _onWeaponFiredAtRate += value;
                }
            }
            remove 
            { 
                _onWeaponFiredAtRate -= value;
            }
        }

        private event Action<Weapon> _onWeaponFiredAtRate;

        [SerializeField, Expandable] private WeaponData _data;
        [SerializeField, ReadOnly] private bool _isFireing = false;
        [SerializeField, ReadOnly] private Transform _firePointTransform;

        private void Awake()
        {
            if (_data == null)
            {
                Debug.LogError("Select Weapon Data");
                return;
            }

            UpdateSprite();
            _firePointTransform = GetFirePointTransform();

            OnWeaponFire += _data.Projectile_Data.FireSingleProjectile;

            _data.LoadAllWeaponMods(this);
        }

        [Button]
        private void UpdateSprite()
        {
            SpriteRenderer spriteRenderer = GetComponent<SpriteRenderer>();
            _data.SpriteData.SetupSprite(spriteRenderer);
        }

        private Transform GetFirePointTransform()
        {
            FirePoint firePoint = GetComponentInChildren<FirePoint>();
            return firePoint.transform;
        }


        public void Equip(GameObject Parent)
        {
            AttachmentManager AttachmentManager = Parent.GetComponentInParent<AttachmentManager>();

            if (AttachmentManager == null)
                return;

            Debug.Log("Equip: " + _data.WeaponName);
            AttachmentManager.AddAttachmentToSlot(gameObject);
        }

        public void Interact(GameObject InteractingGameObject)
        {
            Equip(InteractingGameObject);
        }

        public void StartUsing(Vector2 touchedPosition)
        {
            _isFireing = true;
            StartCoroutine(WeaponFireAtRate());
            
        }

        public void StopUsing(Vector2 touchedPosition)
        {
            _isFireing = false;
            StopCoroutine(WeaponFireAtRate());
        }

        private IEnumerator WeaponFireAtRate()
        {
            while (_isFireing)
            {
                _onWeaponFiredAtRate?.Invoke(this);
                yield return new WaitForSeconds(_data.FireRate_Data.FireRate);
            }
        }

        public void UsingUpdate(Vector2 touchedPosition)
        {
            Vector3 worldPosition = Camera.main.ScreenToWorldPoint(touchedPosition);
            worldPosition.z = 0;

            Vector3 dir = worldPosition - _firePointTransform.position;
            float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;

            _firePointTransform.rotation = Quaternion.AngleAxis(angle, _firePointTransform.forward);
        }
    }
}