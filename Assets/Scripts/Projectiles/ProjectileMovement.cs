using Ren.Items;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Ren.Weapon.Projectile
{
    [RequireComponent(typeof(Rigidbody2D))]
    [RequireComponent(typeof(CapsuleCollider2D))]
    public class ProjectileMovement : MonoBehaviour
    {
        [SerializeField] private ProjectileData _projectileData;

        private Rigidbody2D rb;
        private CapsuleCollider2D capsuleCollider;
        private bool _projectileActive = true;

        private void Awake()
        {
            rb = GetComponent<Rigidbody2D>();
            capsuleCollider = GetComponent<CapsuleCollider2D>();
            _projectileData.Sprite_Data.SetupSprite(GetComponent<SpriteRenderer>());

            gameObject.SetActive(true);
            _projectileActive = true;
        }

        private void OnEnable()
        {
            _projectileActive = true;
            StartCoroutine(UpdateLifeTime());
            //rb.velocity = transform.up * _projectileData.MovementSpeed;
        }

        private void OnDisable()
        {
            _projectileActive = false;
        }

        public void SetProjectileVelocityDirection(Vector2 NewDirection)
        {
            NewDirection.Normalize();

            rb.velocity = NewDirection * _projectileData.MovementSpeed;
            transform.up = rb.velocity;
            Debug.DrawRay(transform.position, rb.velocity, Color.green, 5f);
        }

        private IEnumerator UpdateLifeTime()
        {
            yield return new WaitForSeconds(_projectileData.ProjectileLifeTime);
            Destroy(gameObject);
        }
    }
}
