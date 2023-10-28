using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Ren.StateMachine
{
    public interface IStateMachineUpdate
    {
        public void RecieaveStateMachineUpdate(FiniteStateMachineController controller);

    }
}