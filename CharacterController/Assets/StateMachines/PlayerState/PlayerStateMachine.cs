using UnityEngine;
using UnityEngine.InputSystem;
using System.Collections.Generic;
public class PlayerStateMachine : MonoBehaviour
{
    //maybe find a way to not make all of these public.

    //retains all the variables as this has the greater context of everything.
    //PlayerInputHandler inputHandler;
    PlayerInputs _playerInput;
    public CharacterController _chrController;
    Vector2 _currentMovementInput;
    Vector3 _currentMovement;
    bool _isMovementPressed;

    public float _movementSpeed = 1f;
    public float _rotationFactorPerFrame = 10f;

    PlayerBaseState _currentState;
    PlayerBaseState _previousState;
    playerStateFactory _states;

    //getters and setters
    public PlayerBaseState CurrentState { get { return _currentState; } set { _currentState = value; } }
    public bool IsMovementPressed {get {return _isMovementPressed;} }

    public Vector2 CurrentMovementInput { get { return _currentMovementInput; } }
    public Vector3 CurrentMovement { get { return _currentMovement; } set { _currentMovement = value; } }
    private void Awake()
    {
        _playerInput = new PlayerInputs();
        _chrController = GetComponent<CharacterController>();

        _states = new playerStateFactory(this);
        _currentState = _states.Grounded();
        _currentState.EnterState();

        _playerInput.CharacterControls.Move.started += OnMovementInput;
        _playerInput.CharacterControls.Move.canceled += OnMovementInput;
        _playerInput.CharacterControls.Move.performed += OnMovementInput;
    }

    private void Update()
    {
        //print("current state:" + _currentState.ToString());
        _currentState.UpdateStates();
    }
    void OnMovementInput(InputAction.CallbackContext context)
    {
        _currentMovementInput = context.ReadValue<Vector2>();
        //_currentMovement.x = _currentMovementInput.x;
        //_currentMovement.z = _currentMovementInput.y;
        _isMovementPressed = _currentMovementInput.x != 0 || _currentMovementInput.y != 0;
        print(_currentMovementInput);
    }

    public void RequestStateChange(PlayerBaseState requestedState)
    {
        var superstate = _states.GetSuperstateForSubState(requestedState);
        if (superstate != null)
        {
            superstate.InitializeSubState();
        }
        else
        {

        }
    }

    public void ForceSuperState(PlayerBaseState newSuper)
    {
        _previousState = _currentState;

        _currentState.ExitState();
        _currentState = newSuper;
        _currentState.EnterState();

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
