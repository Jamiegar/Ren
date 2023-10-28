using NaughtyAttributes;
using Ren.StateMachine;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Ren.AI.Perception
{
    public struct PerceptionResult
    {
        public GameObject PerceptedGameObjects;
        public Vector2 PerceptedPosition;
        public Type PerceptionType;
        public bool HasPerceptedObjects;

        public PerceptionResult(GameObject perceptedObjs, Vector2 perceptedPos, Type perceptType, bool perceptedObj)
        {
            PerceptedGameObjects = perceptedObjs;
            PerceptedPosition = perceptedPos;
            PerceptionType = perceptType;
            HasPerceptedObjects = perceptedObj;
        }
    }

    public abstract class PerceptionAction : ScriptableObject
    {
        [Header("Debug")]
        public bool DebugActive = false;
        [EnableIf("DebugActive")] public Color PerceptionDebugColour = Color.green;

        public abstract PerceptionResult PerformPerceptionTick(PerceptionController perceptionController, FiniteStateMachineController stateMachineController);

        public abstract void OnDisplayDebug(PerceptionController perceptionController);
    }
}