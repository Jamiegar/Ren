using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Ren.Controller
{
    public struct ActionEvents<T>
    {
        public event Action<T> OnInputEventStarted;
        public event Action<T> OnInputEventPerfored;
        public event Action<T> OnInputEventCanceled;

        public void OnActionEventTriggered(InputAction.CallbackContext context, T value)
        {
            switch (context.phase)
            {
                case InputActionPhase.Started:
                    OnInputEventStarted?.Invoke(value);
                    break;

                case InputActionPhase.Performed:
                    OnInputEventPerfored?.Invoke(value); 
                    break;

                case InputActionPhase.Canceled:
                    OnInputEventCanceled?.Invoke(value);
                    break;
            }
        }
    }


    [CreateAssetMenu(fileName = "Player Input Receiver", menuName = "Input Receiver" )]
    public class InputReceiver : ScriptableObject, TopDownCharacterActions.IPlayerActions
    {
        public ActionEvents<InputAction.CallbackContext> MovementInputEvents;
        public ActionEvents<Vector2> ShootInputEvents;

        public TopDownCharacterActions PlayerInputs
        {
            get { return _playerInput; }
        }

        private TopDownCharacterActions _playerInput;

        private void OnEnable()
        {
            if (_playerInput == null)
            {
                _playerInput = new TopDownCharacterActions();
                _playerInput.Player.SetCallbacks(this);
            }

            _playerInput.Player.Enable();
        }

        public void OnMovement(InputAction.CallbackContext context)
        {
            MovementInputEvents.OnActionEventTriggered(context, context);
        }

        public void OnShoot(InputAction.CallbackContext context)
        {
            ShootInputEvents.OnActionEventTriggered(context, context.ReadValue<Vector2>());
        }
    }
}