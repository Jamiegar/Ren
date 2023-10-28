using Ren.Events;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace Ren.Events
{
    public class GameEventListener : MonoBehaviour
    {
        [SerializeField] private GameEvent _gameEvent;

        public UnityEvent<Component, object> Response;
  

        private void OnEnable()
        {
            _gameEvent.RegisterListener(this);
        }

        private void OnDisable()
        {
            _gameEvent.RemoveListener(this);
        }

        public void OnEventRaised(Component sender, object data)
        {
            Response?.Invoke(sender, data);
        }
    }
}