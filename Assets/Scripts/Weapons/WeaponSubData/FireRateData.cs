using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Ren.Weapon
{
    [CreateAssetMenu(fileName = "New Fire Rate Data", menuName = "Weapon/Fire Rate Data")]
    public class FireRateData : ScriptableObject
    {
        public float FireRate = 0.5f;
    }
}