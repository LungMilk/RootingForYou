using NUnit.Framework.Interfaces;
using System;
using System.Collections.Generic;
using System.Threading;
using Unity.Cinemachine;
using UnityEngine;
using UnityEngine.InputSystem;

public enum InteractState
{
    Dialogue,
    Planting,
    NonState,
}
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
    //bool _isInteracting;
    bool _interactionLocked;

    public float _movementSpeed = 1f;
    public float _rotationFactorPerFrame = 10f;

    PlayerBaseState _currentState;
    PlayerBaseState _previousState;
    playerStateFactory _states;

    //interact variables
    public GameObject _inputObject;
    //public bool _isInteractPressed;
    private InputAction _interactAction;
    IInteractable _currentInteractable;
    IInteractable _interactedWith;
    InteractState _foundInteractType;

    public CameraEvent OnCameraOptionFound;
    public CinemachineCamera _foundCamera;

    float _interactTimer = 0f;
    float _interactTime = 1f;
    bool _canInteract = true;

    public bool InteractPressedThisFrame { get; private set; }
    //getters and setters
    public PlayerBaseState CurrentState { get { return _currentState; } set { _currentState = value; } }
    public bool IsMovementPressed {get {return _isMovementPressed;} }
    //public bool IsInteractPressed { get { return _isInteractPressed; } }
    //public bool IsInteracting { get { return _isInteracting; } }
    public bool CanInteract { get { return _canInteract; } set { _canInteract = value; } }
    public Vector2 CurrentMovementInput { get { return _currentMovementInput; } }
    public Vector3 CurrentMovement { get { return _currentMovement; } set { _currentMovement = value; } }

    public InteractState FoundInteractType { get { return _foundInteractType; } }
    private void Awake()
    {
        _playerInput = new PlayerInputs();
        _chrController = GetComponent<CharacterController>();

        _states = new playerStateFactory(this);
        _currentState = _states.Movement();
        _currentState.EnterState();

        _playerInput.CharacterControls.Move.started += OnMovementInput;
        _playerInput.CharacterControls.Move.canceled += OnMovementInput;
        _playerInput.CharacterControls.Move.performed += OnMovementInput;

        _interactAction = _playerInput.CharacterControls.Interact;

        _interactAction.started+= OnInteract;
        _interactAction.canceled += OnInteract;
    }

    private void Update()
    {
        //print("current state:" + _currentState.ToString());
        _currentState.UpdateStates();
    }
    private void LateUpdate()
    {
        InteractPressedThisFrame = false;
    }
    void OnMovementInput(InputAction.CallbackContext context)
    {
        _currentMovementInput = context.ReadValue<Vector2>();
        //_currentMovement.x = _currentMovementInput.x;
        //_currentMovement.z = _currentMovementInput.y;
        _isMovementPressed = _currentMovementInput.x != 0 || _currentMovementInput.y != 0;
        //print(_currentMovementInput);
    }
    //:3
    public void RequestStateChange(PlayerBaseState newState)
    {
        if (_currentState == newState) return;
        _previousState = _currentState;

        _currentState.ExitState();
        _currentState = newState;
        _currentState.EnterState();
    }
    public void ReturnToPreviousState()
    {
        _currentState.ExitState();
        _currentState = _previousState;
        _currentState.EnterState();
    }

    public void ReturnToMovement()
    {
        RequestStateChange(_states.Movement());
    }
    private void OnEnable()
    {
        _playerInput.CharacterControls.Enable();
    }

    private void OnDisable()
    {
        _playerInput.CharacterControls.Disable();
    }

    //stuff copied from the interaction script to homogonize functionality
    //3d interactions based off of the interfaces and the like, just run with it for now

    //we may need an interactable priority system but right now it gets the nearest in the collision
    //we save the interactable as doing a call for getting any on the button press sounds pretty taxing.
    //probs not important
    void OnInteract(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            HandleInteractions();
        }
    }
    private void HandleInteractions()
    {
        if (_currentInteractable == null) return;

        _currentInteractable.Interact();
        OnCameraOptionFound?.Invoke(_foundCamera);

        if (_currentState is PlayerInteractState)
        {
            //may have to change this later as the second press in interact always goes to movement
            //discuss if I want interact to be progressing dialogue as it is simpler.
            //could also probably put it in the planting state as well.
            ReturnToMovement();
            return;
        }

        if (_interactedWith != null) { return;}

        _foundInteractType = _currentInteractable.InteractableType;
        _previousState = _currentState;

        _interactedWith = _currentInteractable;

        if (_foundInteractType != InteractState.NonState)
        {
            _previousState = _currentState;
            RequestStateChange(_states.Interact());
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out IInteractable interactable))
        {
            //print("Found interactable");
            //_canInteract = false;
            _currentInteractable = interactable;
        }
        if (other.TryGetComponent(out ICameraOption cameraOption))
        {
            _foundCamera = cameraOption.CameraOption;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.TryGetComponent(out IInteractable interactable))
        {
            //print("Exit interactable");
            _interactedWith = null;
            if (_currentInteractable == interactable)
            {
                //_canInteract = true;
                _currentInteractable = null;
            }
            if (_foundCamera != null)
            {
                _foundCamera = null;
            }
        }
    }
}
