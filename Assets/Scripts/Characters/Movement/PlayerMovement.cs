using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using Ren.Controller;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private AnimationCurve _movementCurve;
    [SerializeField] private float _movementSpeedMultiplier = 10;
    [SerializeField] private Rigidbody2D _rb;
    [SerializeField] private InputReceiver _playerController;

    private float _movementSpeed;
    private float _movementInputTime;
    private Vector2 _movementInputAxes;

    private bool _facingRight = true;

    private void Awake()
    {
        
    }

    private void OnEnable()
    {
        _playerController.MovementInputEvents.OnInputEventPerfored += StartPlayerMovement;
        _playerController.MovementInputEvents.OnInputEventCanceled += StopPlayerMovement;
    }

    private void OnDisable()
    {
        _playerController.MovementInputEvents.OnInputEventPerfored -= StartPlayerMovement;
        _playerController.MovementInputEvents.OnInputEventCanceled -= StopPlayerMovement;
    }

    private void StartPlayerMovement(InputAction.CallbackContext Context)
    {
        _movementInputAxes = Context.ReadValue<Vector2>();

        HandleFlip();
        StartCoroutine(InputTick(Context));
    }

    private void StopPlayerMovement(InputAction.CallbackContext Context)
    {
        _movementInputAxes = Context.ReadValue<Vector2>();
        StopCoroutine(InputTick(Context));
        _movementInputTime = 0;
    }

    private IEnumerator InputTick(InputAction.CallbackContext Context)
    {
        while (_playerController.PlayerInputs.Player.Movement.IsPressed()) 
        { 
           _movementInputTime += Time.deltaTime;
            yield return null;    
        }
    }

    private void FixedUpdate()
    {
        CalculateMovementSpeed(_movementInputTime);
        HandleTopDownMovement(_movementInputAxes);
    }

    private void CalculateMovementSpeed(float InputDuration)
    {
        _movementSpeed = _movementCurve.Evaluate(InputDuration) * _movementSpeedMultiplier;
    }

    private void HandleTopDownMovement(Vector2 AxisInput)
    {
        _rb.AddForce(AxisInput * _movementSpeed);
    }

    private void HandleFlip()
    {
        if (_movementInputAxes.x > 0)
        {
            _facingRight = false;
            transform.localScale = new Vector3(1, 1, 1);
        }
        else
        {
            _facingRight = true;
            transform.localScale = new Vector3(-1, 1, 1);
        }
    }
}
