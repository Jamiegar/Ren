
using NaughtyAttributes;
using Ren.StateMachine;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using static UnityEngine.RuleTile.TilingRuleOutput;

namespace Ren.AI.Perception
{
    [CreateAssetMenu(fileName = "SightPerception", menuName = "ScriptableObject/AI/Perception/Sight")]
    public class SightPerception : PerceptionAction
    {
        [Header("Sight")]

        [SerializeField, Tag] private List<string> _sightPerceptionTargetsTags;
        [SerializeField, Range(1, 25)] private float _sightRadius = 5;
        
        [SerializeField, Range(0, 180), Tooltip("The angle of the sight cone")]
        private float _visionConeAngle;

        [SerializeField] private ContactFilter2D _sightContactFilter;

        public override PerceptionResult PerformPerceptionTick(PerceptionController perceptionController, FiniteStateMachineController stateMachineController)
        {
            bool hasSeenTarget = PerceptTargetObjects(stateMachineController, out List<GameObject> targets);

            if (!hasSeenTarget)
                return new PerceptionResult(null, Vector2.zero, null, false);

        #if UNITY_EDITOR
            if(DebugActive)
                Debug.DrawLine(perceptionController.transform.position, targets[0].transform.position, Color.white, 0.1f);
        #endif
            return new PerceptionResult(targets[0], targets[0].transform.position, GetType(), hasSeenTarget);
        }

        private bool PerceptTargetObjects(FiniteStateMachineController controller, out List<GameObject> perceivedTargets)
        {
            List<Collider2D> results = new List<Collider2D>();
            int hitResults = Physics2D.OverlapCircle(controller.Rb.position, _sightRadius, _sightContactFilter, results);

            perceivedTargets = new List<GameObject>();

            if (hitResults <= 0)
            {
                perceivedTargets = null;
                return false;
            }


            foreach (Collider2D collider in results)
            {
                foreach (string tag in _sightPerceptionTargetsTags)
                {
                    if (!collider.gameObject.CompareTag(tag))
                        continue;

                    Vector2 targetDir = (collider.transform.position - controller.transform.position).normalized;
                    float targetDot = Vector2.Dot(controller.transform.right, targetDir);

                    float angle = Mathf.Acos(targetDot) * Mathf.Rad2Deg;

                    if (angle <= _visionConeAngle)
                        perceivedTargets.Add(collider.gameObject);

                }
            }

            if (perceivedTargets.Count > 0)
                return true;

            return false;
        }

        public override void OnDisplayDebug(PerceptionController perceptionController)
        {
            Quaternion upRayRot = Quaternion.AngleAxis(-_visionConeAngle + 180, Vector3.forward);
            Vector2 upRayDir = upRayRot * perceptionController.transform.right;

            Handles.DrawSolidArc(perceptionController.transform.position, perceptionController.transform.forward, perceptionController.transform.right, _visionConeAngle, _sightRadius);
            Handles.DrawSolidArc(perceptionController.transform.position, perceptionController.transform.forward, upRayDir * -1, _visionConeAngle, _sightRadius);
        }
    }
}