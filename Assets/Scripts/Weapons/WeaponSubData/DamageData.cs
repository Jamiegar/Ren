using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Ren.Weapon
{
    [CreateAssetMenu(fileName = "New Damage Data", menuName = "Weapon/Damage Data")]
    public class DamageData : ScriptableObject
    {
        public float BaseDamage = 1;
    }
}