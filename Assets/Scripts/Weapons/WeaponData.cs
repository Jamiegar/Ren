using NaughtyAttributes;
using Ren.Items;
using Ren.Weapon.Mods;
using Ren.Weapon.Projectile;
using System.Collections.Generic;
using UnityEngine;

namespace Ren.Weapon
{
    [CreateAssetMenu(fileName = "New Weapon Data", menuName = "Weapon/Weapon Data")]
    public class WeaponData : ScriptableObject
    {
        public string WeaponName = "New Weapon";
        public GameObject WeaponPrefab;

        [Space]
        [Header("Required Data")]
        [Expandable] public ItemSpriteData SpriteData;
        [Expandable] public DamageData Damage_Data;
        [Expandable] public ProjectileData Projectile_Data;
        [Expandable] public FireRateData FireRate_Data;

        [Space]
        [Header("Mods")]
        [SerializeField] private List<Mod> Mods;


        public void LoadAllWeaponMods(Weapon weapon)
        {
            foreach(var mod in Mods) 
            {
                mod.LoadMod(weapon);
            }
        }

        
    }
}