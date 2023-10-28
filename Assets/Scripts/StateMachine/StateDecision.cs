using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Ren.StateMachine
{
    public abstract class StateDecision : ScriptableObject
    {
        public abstract bool ExecuteDecision(FiniteStateMachineController controller);
    }
}