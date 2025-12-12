using UnityEngine;
using System.Collections.Generic;
using System.Collections;
using UnityEngine.InputSystem;
public class MovementController : MonoBehaviour
{
    PlayerInputs _playerInput;
    CharacterController _chrController;
    Vector2 _currentmovementInput;
    Vector3 _currentMovement;
    Vector3 _currentRunMovement;
    bool _isMovementPressed;

    public float _movementSpeed = 1f;
    public float _sprintSpeed = 3f;
    float _rotationFactorPerFrame = 10f;

    bool _isRunPressed;
    //bool hasSprint;

    private void Awake()
    {
        _playerInput = new PlayerInputs();
        _chrController = GetComponent<CharacterController>();

        _playerInput.CharacterControls.Move.started += OnMovementInput;
        _playerInput.CharacterControls.Move.canceled += OnMovementInput;
        //should be controller centric
        _playerInput.CharacterControls.Move.performed += OnMovementInput;
        _playerInput.CharacterControls.Run.started += OnRun;
        _playerInput.CharacterControls.Run.canceled += OnRun;
    }

    void HandleGravity()
    {
        if (_chrController.isGrounded)
        {
            float groundedGravity = -.05f;
            _currentRunMovement.y = groundedGravity;
            _currentMovement.y = groundedGravity;
        }
        else
        {
            float gravity = -9.8f;
            _currentMovement.y += gravity;
            _currentRunMovement.y += gravity;

        }
    }
    void OnRun (InputAction.CallbackContext context)
    {
        _isRunPressed = context.ReadValueAsButton();
    }
    void HandleRotation()
    {
        Vector3 positionTolookAt;
        positionTolookAt.x = _currentMovement.x;
        positionTolookAt.y = 0.0f;
        positionTolookAt.z = _currentMovement.z;

        Quaternion currentRotation = transform.rotation;
        //Quaternion targetRotation = Quaternion.LookRotation(positionTolookAt);

        if (_isMovementPressed)
        {
            Quaternion targetRotation = Quaternion.LookRotation(positionTolookAt);
            transform.rotation = Quaternion.Slerp(currentRotation,targetRotation, _rotationFactorPerFrame * Time.deltaTime);
        }
    }
    void OnMovementInput(InputAction.CallbackContext context)
    {
        //Debug.Log(context.ReadValue<Vector2>());
        _currentmovementInput = context.ReadValue<Vector2>();
        _currentMovement.x = _currentmovementInput.x;
        _currentMovement.z = _currentmovementInput.y;
        _currentRunMovement.x = _currentmovementInput.x * _sprintSpeed;
        _currentRunMovement.z = _currentmovementInput.y * _sprintSpeed;
        _isMovementPressed = _currentmovementInput.x != 0 || _currentmovementInput.y != 0;
    }

    void Update()
    {
        Debug.Log(_currentMovement);
        HandleRotation();
        //chrController.Move(currentMovement);
        //look into simple move
        if (_isRunPressed)
        {
            _chrController.Move(_currentRunMovement * _movementSpeed * Time.deltaTime);
        }
        else
        {
            _chrController.Move(_currentMovement * _movementSpeed * Time.deltaTime);
        }
    }

    private void OnEnable()
    {
        _playerInput.CharacterControls.Enable();
    }

    private void OnDisable()
    {
        _playerInput.CharacterControls.Disable();
    }
}
