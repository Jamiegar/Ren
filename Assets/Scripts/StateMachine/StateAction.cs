using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Ren.StateMachine
{
    public abstract class StateAction : ScriptableObject
    {
        public abstract void OnEnterStateAction(FiniteStateMachineController controller);
        public abstract void PerformActionTick(FiniteStateMachineController controller);
        public abstract void OnExitStateAction(FiniteStateMachineController controller);
    }
}