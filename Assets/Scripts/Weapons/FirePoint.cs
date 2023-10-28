using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Ren.Weapon
{
    [Serializable]
    public struct FirePointData
    {
        public Vector2 Position;
        public Quaternion Rotation;

        public FirePointData(Vector2 position, Quaternion rotation)
        {
            Position = position;
            Rotation = rotation;
        }
    }

    public class FirePoint : MonoBehaviour
    {
        public FirePointData GetFirePointData()
        {
            return new FirePointData(transform.position, transform.rotation);
        }
    }
}