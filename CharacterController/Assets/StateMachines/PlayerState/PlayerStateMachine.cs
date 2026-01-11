using UnityEngine;
using UnityEngine.InputSystem;
using System.Collections.Generic;
public class PlayerStateMachine : MonoBehaviour
{
    //maybe find a way to not make all of these public.

    //retains all the variables as this has the greater context of everything.
    PlayerInputHandler inputHandler;
    PlayerInputs _playerInput;
    public CharacterController _chrController;
    Vector2 _currentMovementInput;
    Vector3 _currentMovement;
    bool _isMovementPressed;

    public float _movementSpeed = 1f;
    public float _rotationFactorPerFrame = 10f;

    PlayerBaseState _currentState;
    playerStateFactory _states;

    //getters and setters
    public PlayerBaseState CurrentState { get { return _currentState; } set { _currentState = value; } }
    public bool IsMovementPressed {get {return _isMovementPressed;} }
    public Vector3 CurrentMovement { get { return _currentMovement; } set { _currentMovement = value; } }
    private void Awake()
    {
        inputHandler = new PlayerInputHandler();
        _playerInput = inputHandler.playerInputHandler;
        _chrController = GetComponent<CharacterController>();

        _states = new playerStateFactory(this);
        _currentState = _states.Grounded();
        _currentState.EnterState();

        inputHandler.playerInputHandler.CharacterControls.Move.started += OnMovementInput;
        inputHandler.playerInputHandler.CharacterControls.Move.canceled += OnMovementInput;
    }

    private void Update()
    {
        _currentState.UpdateState();
    }
    void OnMovementInput(InputAction.CallbackContext context)
    {
        _currentMovementInput = context.ReadValue<Vector2>();
        _isMovementPressed = _currentMovementInput.x != 0 || _currentMovementInput.y != 0;
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
