using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Ren.Events
{

    [CreateAssetMenu(fileName ="New Game Event", menuName ="Game Event")]
    public class GameEvent : ScriptableObject
    {
        private List<GameEventListener> Listeners = new List<GameEventListener>();

        public void Raise(Component sender, object data)
        {
            foreach(GameEventListener listener in Listeners) 
            { 
                listener.OnEventRaised(sender, data);
            }
        }

        public void RegisterListener(GameEventListener listener)
        {
            if(!Listeners.Contains(listener))
                Listeners.Add(listener);
        }

        public void RemoveListener(GameEventListener listener) 
        { 
            if(Listeners.Contains(listener))
                Listeners.Remove(listener);
        }
    }
}
