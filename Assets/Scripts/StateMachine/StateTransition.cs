using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Ren.StateMachine
{
    [System.Serializable]
    public class StateTransition
    {
        [SerializeField] private StateDecision _decision;
        public FiniteState TrueState;
        public FiniteState FalseState;

        public bool DecisionSucceeded(FiniteStateMachineController controller)
        {
            return _decision.ExecuteDecision(controller);
        }
    }
}