using UnityEngine;
using UnityEngine.InputSystem;
using System.Collections.Generic;
public class PlayerStateMachine : MonoBehaviour
{
    //retains all the variables as this has the greater context of everything.
    PlayerInputs _playerInput;
    CharacterController _chrController;
    Vector2 _currentMovementInput;
    Vector3 _currentMovement;
    Vector3 _currentRunMovement;
    bool _isMovementPressed;

    public float _movementSpeed = 1f;
    public float _sprintSpeed = 3f;
    float _rotationFactorPerFrame = 10f;

    bool _isRunPressed;

    PlayerBaseState _currentState;
    playerStateFactory _states;

    //getters and setters
    public PlayerBaseState CurrentState { get { return _currentState; } set { _currentState = value; } }
    public bool IsMovementPressed {get {return _isMovementPressed;} }
    public bool IsRunPressed { get { return _isRunPressed; } }
    public float CurrentMovementY { get { return _currentMovement.y; } set { _currentMovement.y = value; } }
    private void Awake()
    {
        _playerInput = new PlayerInputs();
        _chrController = GetComponent<CharacterController>();

        _states = new playerStateFactory(this);
        _currentState = _states.Grounded();
        _currentState.EnterState();

        _playerInput.CharacterControls.Move.started += OnMovementInput;
        _playerInput.CharacterControls.Move.canceled += OnMovementInput;
        //should be controller centric
        _playerInput.CharacterControls.Move.performed += OnMovementInput;
        _playerInput.CharacterControls.Run.started += OnRun;
        _playerInput.CharacterControls.Run.canceled += OnRun;
    }

    void OnMovementInput(InputAction.CallbackContext context)
    {
        _currentMovementInput = context.ReadValue<Vector2>();
        _isMovementPressed = _currentMovementInput.x != 0 || _currentMovementInput.y != 0;
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
            transform.rotation = Quaternion.Slerp(currentRotation, targetRotation, _rotationFactorPerFrame * Time.deltaTime);
        }
    }

    private void Update()
    {
        //Debug.Log(_currentMovement);
        HandleRotation();
        _currentState.UpdateState();
        _chrController.Move(_currentMovement *Time.deltaTime * _movementSpeed);
        //look into simple move
    }
    void OnRun(InputAction.CallbackContext context)
    {
        _isRunPressed = context.ReadValueAsButton();
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
