using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem.XR;

namespace Ren.StateMachine
{
    [CreateAssetMenu(fileName = "NewState", menuName = "ScriptableObject/StateMachine/State")]
    public class FiniteState : ScriptableObject
    {
        [SerializeField] private StateAction[] _actions;
        [SerializeField] private StateTransition[] _transitions;

        public void OnEnterState(FiniteStateMachineController controller)
        {
            foreach (var action in _actions)
            {
                action.OnEnterStateAction(controller);
            }
        }

        public void UpdateState(FiniteStateMachineController controller)
        {
            CompleteActions(controller);
            EvaluateTransitions(controller);
        }

        public void OnExitState(FiniteStateMachineController controller)
        {
            foreach (var action in _actions)
            {
                action.OnExitStateAction(controller);
            }
        }

        private void CompleteActions(FiniteStateMachineController controller)
        {
            foreach (var action in _actions) 
            {
                action.PerformActionTick(controller);
            }
        }

        private void EvaluateTransitions(FiniteStateMachineController controller)
        {
            foreach (var transition in _transitions) 
            {
                bool decisionHasSuccess = transition.DecisionSucceeded(controller);

                if (decisionHasSuccess) 
                {
                    controller.TransitionState(transition.TrueState);
                }
                else
                {
                    controller.TransitionState(transition.FalseState);
                }
            }
        }

    }
}