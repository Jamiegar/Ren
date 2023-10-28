using NaughtyAttributes;
using Pathfinding;
using Ren.AI;
using Ren.AI.Perception;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace Ren.StateMachine
{
    [RequireComponent(typeof(Rigidbody2D), typeof(Seeker))]
    public class FiniteStateMachineController : MonoBehaviour
    {
        [Header("AI Data")]
        [SerializeField, Required] private EnemyData _enemyData;
        [SerializeField, Required] private GameObject _eyesObj;

        [Header("States")]
        [SerializeField, Required] private FiniteState _startingState;
        [SerializeField, ReadOnly] private FiniteState _currentActiveState;

        [Header("Pathfinding & Movement")]
        [SerializeField] private Seeker _seeker;
        [SerializeField] private Rigidbody2D _rb;
        
        [Header("Tick")]
        [SerializeField] private float _tickRate = 0.1f;

        [Header("Activity")]
        [SerializeField, ReadOnly] private bool _stateMachineActive = true;

        private IStateMachineUpdate[] _updateObjects;

        #region Getters/Setters
        public Seeker seeker { get { return _seeker; } private set { _seeker = value; } }
        public Rigidbody2D Rb { get { return _rb; } private set { _rb = value;} }
        public EnemyData EnemyData { get { return _enemyData; } private set { _enemyData = value; } }
        public GameObject EnemyEyes { get { return _eyesObj; } private set { _eyesObj = value; } }
        #endregion

        private void Awake()
        {
            /* Assign seeker and rididbody if not already assigned */
            _seeker = _seeker != null ? _seeker : GetComponent<Seeker>();
            _rb = _rb != null ? _rb : GetComponent<Rigidbody2D>();

            /*
             * NOTE !!
             * Only gets update objects that are on this gameobject
             */
            _updateObjects = GetComponents<IStateMachineUpdate>();

            _currentActiveState = _startingState;
            _currentActiveState.OnEnterState(this);
        }

        private void OnEnable()
        {
            StartCoroutine(StateMachineUpdate());
        }

        private void OnDisable()
        {
            StopCoroutine(StateMachineUpdate());
        }

        private IEnumerator StateMachineUpdate()
        {
            while(_stateMachineActive)
            {
                UpdateStateObjects();
                yield return new WaitForSeconds(_tickRate);
            }
        }

        private void FixedUpdate()
        {
            _currentActiveState?.UpdateState(this);
        }

        private void UpdateStateObjects()
        {
            if (_updateObjects.Length <= 0)
                return;

            foreach(IStateMachineUpdate obj in _updateObjects)
            {
                obj.RecieaveStateMachineUpdate(this);
            }
        }

        public void TransitionState(FiniteState newState)
        {
            if (newState == null)
                return;

            if (newState == _currentActiveState)
                return;

            
            _currentActiveState.OnExitState(this);
            _currentActiveState = newState;
            _currentActiveState.OnEnterState(this);
        }
    }
}