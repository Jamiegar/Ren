using Ren.Items;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

namespace Ren.Weapon.Projectile
{
    [CreateAssetMenu(fileName = "New Projectile Data", menuName = "Weapon/Projectile Data")]
    public class ProjectileData : ScriptableObject
    {
        public GameObject ProjectilePrefab;
        public float MovementSpeed;
        public float ProjectileLifeTime = 5;
        public ItemSpriteData Sprite_Data;

        public GameObject SpawnProjectile(UnityEngine.Transform transform)
        {
            return Instantiate(ProjectilePrefab, transform.position, transform.rotation);
        }

        public GameObject SpawnProjectile(Vector2 Position, Quaternion Rotation)
        {
            return Instantiate(ProjectilePrefab, Position, Rotation);
        }

        public GameObject SpawnProjectile(FirePointData firePoint)
        {
            return Instantiate(ProjectilePrefab, firePoint.Position, firePoint.Rotation);
        }

        public void FireSingleProjectile(Weapon weapon)
        {
            GameObject proj = SpawnProjectile(weapon.FirePointTransform.position, Quaternion.identity);
            proj.GetComponent<ProjectileMovement>().SetProjectileVelocityDirection(weapon.FirePointTransform.right);
            
        }
    }
}