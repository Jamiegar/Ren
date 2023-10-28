using NaughtyAttributes;
using Ren.AI.Perception;
using System.Collections.Generic;
using UnityEngine;

namespace Ren.AI
{
    [CreateAssetMenu(fileName = "NewEnemyData", menuName = "ScriptableObject/AI/EnemyData")]
    public class EnemyData : ScriptableObject
    {
        [Header("Movement")]
        public float PatrolSpeed = 350;
        public float RunSpeed = 400;

        [Space(20f)]

        [Header("Pathfinding")]
        public float WaypointTargetDistance = 1.5f;
    }
}