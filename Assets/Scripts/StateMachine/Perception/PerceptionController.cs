using NaughtyAttributes;
using Ren.StateMachine;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace Ren.AI.Perception
{
    public class PerceptionController : MonoBehaviour, IStateMachineUpdate
    {
        [SerializeField, Expandable] private PerceptionAction[] _perceptionActions;

        public PerceptionAction[] PerceptionActions { get { return _perceptionActions; } private set { _perceptionActions = value; } }

        public void RecieaveStateMachineUpdate(FiniteStateMachineController controller)
        {
            foreach (var action in _perceptionActions) 
            {
                action.PerformPerceptionTick(this, controller);
            }
        }
    }

#if UNITY_EDITOR
    [CustomEditor(typeof(PerceptionController))]
    public class PerceptionControllerEditor : Editor
    {
        public void OnSceneGUI()
        {
            var perceptionCont = target as PerceptionController;

            foreach(var debugPerception in perceptionCont.PerceptionActions)
            {
                if(debugPerception == null)
                    continue;

                if (debugPerception.DebugActive == false)
                    continue;

                Handles.color = debugPerception.PerceptionDebugColour;
                debugPerception.OnDisplayDebug(perceptionCont);
            }
        }
    }
#endif
}
