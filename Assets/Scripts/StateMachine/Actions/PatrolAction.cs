using NaughtyAttributes;
using Pathfinding;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Ren.StateMachine
{
    [CreateAssetMenu(fileName = "PatrolAction", menuName = "ScriptableObject/StateMachine/Actions/Patrol")]
    public class PatrolAction : StateAction
    {
        [SerializeField] private float _patrolPathRadius = 10f;
        [SerializeField, MinMaxSlider(0, 60)] private Vector2 _waitAtWaypointTime;
        
        private Path _patrolPath;
        private int _currentPathWaypoint = 0;
        private bool _canFollowPath = false;
        private event Action<FiniteStateMachineController> OnReachedPatrolPointEnd;

        public override void OnEnterStateAction(FiniteStateMachineController controller)
        {
            GenerateRandomPatrolPoint(controller);
            OnReachedPatrolPointEnd += OnReachedEnd;
            _currentPathWaypoint = 0;
        }

        public override void OnExitStateAction(FiniteStateMachineController controller)
        {
            throw new NotImplementedException();
        }
        public override void PerformActionTick(FiniteStateMachineController controller)
        {
            Patrol(controller);
        }

        private void Patrol(FiniteStateMachineController controller) 
        {
            if (controller.seeker.IsDone() && _canFollowPath)
            {
                NavigateAlongPatrolPath(controller);
            }
        }

        private void NavigateAlongPatrolPath(FiniteStateMachineController controller)
        {
            if (CheckPathComplete())
            {
                OnReachedPatrolPointEnd.Invoke(controller);
                return;
            }

            Vector2 travelDir = ((Vector2)_patrolPath.vectorPath[_currentPathWaypoint] - controller.Rb.position).normalized;
            Vector2 force = travelDir * controller.EnemyData.PatrolSpeed * Time.deltaTime;

            controller.Rb.AddForce(force);

            if (IsAtCurrentWaypoint(controller))
                _currentPathWaypoint++;
        }

        private void GenerateRandomPatrolPoint(FiniteStateMachineController controller)
        {
            Vector2 randPoint = UnityEngine.Random.insideUnitCircle * _patrolPathRadius;

            controller.seeker.StartPath(controller.Rb.position, randPoint, OnPathGeneratedComplete);
        }

        private void OnPathGeneratedComplete(Path generatedPath)
        {
            if(!generatedPath.error)
            {
                _patrolPath = generatedPath;
                _currentPathWaypoint = 0;
                _canFollowPath = true;
            }
        }

        private bool CheckPathComplete()
        {
            if(_currentPathWaypoint >= _patrolPath.vectorPath.Count)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        private bool IsAtCurrentWaypoint(FiniteStateMachineController controller)
        {
            float distance = Vector2.Distance(controller.Rb.position, _patrolPath.vectorPath[_currentPathWaypoint]);

            if(distance < controller.EnemyData.WaypointTargetDistance)
            {
                return true;
            }
            return false;
        }

        private void OnReachedEnd(FiniteStateMachineController controller)
        {
            _canFollowPath = false;
            controller.StartCoroutine(WaitAtPoint(controller));
        }

        private IEnumerator WaitAtPoint(FiniteStateMachineController controller)
        {
            float waitTime = UnityEngine.Random.Range(_waitAtWaypointTime.x, _waitAtWaypointTime.y);
            yield return new WaitForSeconds(waitTime);
            GenerateRandomPatrolPoint(controller);
        }
    }
}