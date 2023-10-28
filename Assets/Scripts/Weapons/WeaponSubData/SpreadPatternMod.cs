using Ren.Weapon.Projectile;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Ren.Weapon.Mods
{
    [CreateAssetMenu(fileName = "New Spread Pattern Data", menuName = "Weapon/Spread Pattern Data")]
    public class SpreadPatternMod : Mod
    {
        public int NumberOfProjectiles = 1;
        [Range(0, 359)] public float DirectionalSpread = 0;

        public override void LoadMod(Weapon weapon)
        {
            Debug.Log("Loaded Mod: " + name);
            weapon.OnWeaponFire += CalculateWeaponSpreadPattern;

            base.LoadMod(weapon);
        }

        public void CalculateWeaponSpreadPattern(Weapon weapon)
        {
            Vector2 targetDir = weapon.FirePointTransform.right;

            float targetAngle = Mathf.Atan2(targetDir.y, targetDir.x) * Mathf.Rad2Deg;
            float halfAngleSpread = DirectionalSpread / 2f;
            float angleStep = DirectionalSpread / (NumberOfProjectiles - 1);

            float startAngle = targetAngle - halfAngleSpread;
            float endAngle = targetAngle + halfAngleSpread;
            float currentAngle = startAngle;


            for (int i = 0; i <= NumberOfProjectiles - 1; i++)
            {
                float directionX = weapon.FirePointTransform.position.x + 0.1f * Mathf.Cos(currentAngle * Mathf.Deg2Rad);
                float directionY = weapon.FirePointTransform.position.y + 0.1f * Mathf.Sin(currentAngle * Mathf.Deg2Rad);

                Vector3 position = new Vector3(directionX, directionY, 0);

                GameObject proj = weapon.Data.Projectile_Data.SpawnProjectile(position, Quaternion.identity);
                proj.GetComponent<ProjectileMovement>().SetProjectileVelocityDirection(position - weapon.FirePointTransform.position);


                Debug.DrawRay(weapon.FirePointTransform.position, (position - weapon.FirePointTransform.position) * 5f, Color.green, 5f);
                currentAngle += angleStep;
            }
        }
    }
}
