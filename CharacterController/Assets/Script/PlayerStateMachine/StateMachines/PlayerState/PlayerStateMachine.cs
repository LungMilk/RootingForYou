using ScriptableObjects;
using System.Collections.Generic;
using Unity.Cinemachine;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Events;

public enum InteractState
{
    Dialogue,
    Planting,
    NonState,
}

public class PlayerStateMachine : MonoBehaviour
{
    [Header("Core References")]
    public Animator _animator;
    public CharacterController _chrController;

    [Header("Camera References")]
    public Transform _cameraTransform;

    [Header("Movement Settings")]
    public float _walkSpeed = 3f;
    public float _runSpeed = 5.5f;
    public float _rotationFactorPerFrame = 12f;

    [Header("Gravity Settings")]
    public float _gravity = -20f;
    public float _groundedGravity = -2f;

    private PlayerInputs _playerInput;

    private Vector2 _currentMovementInput;
    private Vector2 _currentLookInput;
    private Vector3 _currentMovement;
    private bool _isMovementPressed;
    private bool _isRunPressed;
    private float _verticalVelocity;

    private PlayerBaseState _currentState;
    private PlayerBaseState _previousState;
    private playerStateFactory _states;

    [Header("Interaction")]
    public PlayerInputHandler _inputHandler;
    public GameObject _inputObject;
    private InputAction _interactAction;
    private Interactable _currentInteractable;
    private Interactable _interactedWith;
    private InteractState _foundInteractType;

    public CameraEvent OnCameraOptionFound;
    public CinemachineCamera _foundCamera;

    public bool _canInteract = true;

    [Header("Planting")]
    public PlantCollectionSO _plantCollection;
    public PlantObjectSO _selectedPlantObject;
    private PlacedObjectTypeSO.Dir _dir = PlacedObjectTypeSO.Dir.Down;

    [Header("Audio")]
    public List<SoundEffectSO> _soundEffects;

    [Header("Events")]
    public UnityEvent nearInteractable;
    public UnityEvent interactedWithSomething;
    public UnityEvent exitInteractable;
    [Tooltip("When the player walks away from it")]
    public UnityEvent walkAwayInteractable;

    public bool InteractPressedThisFrame { get; private set; }

    public PlayerBaseState CurrentState { get => _currentState; set => _currentState = value; }
    public bool IsMovementPressed => _isMovementPressed;
    public bool IsRunPressed => _isRunPressed;
    public Vector2 CurrentMovementInput => _currentMovementInput;
    public Vector2 CurrentLookInput => _currentLookInput;
    public Vector3 CurrentMovement { get => _currentMovement; set => _currentMovement = value; }
    public float VerticalVelocity { get => _verticalVelocity; set => _verticalVelocity = value; }
    public InteractState FoundInteractType => _foundInteractType;
    public GameObject InputObject => _inputObject;
    public Transform CameraTransform => _cameraTransform;

    private void Start()
    {
        _inputHandler = GetComponent<PlayerInputHandler>();
        _playerInput = _inputHandler.playerInputHandler;
        if (_playerInput == null)
        {
            Debug.LogError("playerInputHandler is NULL on PlayerInputHandler!");
        }
        _chrController = GetComponent<CharacterController>();

        if (_cameraTransform == null && Camera.main != null)
        {
            _cameraTransform = Camera.main.transform;
        }

        _states = new playerStateFactory(this);
        _currentState = _states.Movement();
        _currentState.EnterState();
        _playerInput.CharacterControls.Enable();
        _playerInput.CharacterControls.Move.started += OnMovementInput;
        _playerInput.CharacterControls.Move.performed += OnMovementInput;
        _playerInput.CharacterControls.Move.canceled += OnMovementInput;

        _playerInput.CharacterControls.Look.started += OnLookInput;
        _playerInput.CharacterControls.Look.performed += OnLookInput;
        _playerInput.CharacterControls.Look.canceled += OnLookInput;

        _playerInput.CharacterControls.Run.started += OnRunInput;
        _playerInput.CharacterControls.Run.performed += OnRunInput;
        _playerInput.CharacterControls.Run.canceled += OnRunInput;

        _interactAction = _playerInput.CharacterControls.Interact;
        _interactAction.started += OnInteract;
    }

    private void Update()
    {
        _currentState.UpdateStates();
    }

    private void LateUpdate()
    {
        InteractPressedThisFrame = false;
    }

    private void OnDisable()
    {
        _playerInput.CharacterControls.Disable();
    }

    private void OnDestroy()
    {
        _playerInput.CharacterControls.Move.started -= OnMovementInput;
        _playerInput.CharacterControls.Move.performed -= OnMovementInput;
        _playerInput.CharacterControls.Move.canceled -= OnMovementInput;

        _playerInput.CharacterControls.Look.started -= OnLookInput;
        _playerInput.CharacterControls.Look.performed -= OnLookInput;
        _playerInput.CharacterControls.Look.canceled -= OnLookInput;

        _playerInput.CharacterControls.Run.started -= OnRunInput;
        _playerInput.CharacterControls.Run.performed -= OnRunInput;
        _playerInput.CharacterControls.Run.canceled -= OnRunInput;

        _interactAction.started -= OnInteract;
    }

    private void OnMovementInput(InputAction.CallbackContext context)
    {
        _currentMovementInput = context.ReadValue<Vector2>();
        _isMovementPressed = _currentMovementInput.sqrMagnitude > 0.0001f;
    }

    private void OnLookInput(InputAction.CallbackContext context)
    {
        _currentLookInput = context.ReadValue<Vector2>();
    }

    private void OnRunInput(InputAction.CallbackContext context)
    {
        _isRunPressed = context.ReadValueAsButton();
    }

    private void OnInteract(InputAction.CallbackContext context)
    {
        if (!context.started) return;

        InteractPressedThisFrame = true;
        HandleInteractions();
    }

    public void RequestStateChange(PlayerBaseState newState)
    {
        //print($"State change: {_currentState} -> {newState}");
        if (_currentState == newState) return;

        _previousState = _currentState;
        _currentState.ExitState();
        _currentState = newState;
        _currentState.EnterState();
    }

    public void ReturnToPreviousState()
    {
        if (_previousState == null || _previousState == _currentState)
            return;


        _currentState.ExitState();
        _currentState = _previousState;
        _currentState.EnterState();
    }

    public void ReturnToMovement()
    {
        RequestStateChange(_states.Movement());
    }

    private void HandleInteractions()
    {
        if (CameraManager.Instance.brain.IsBlending) return;

        if (_currentInteractable == null) return;

        if (_currentState is PlayerInteractState)
        {
            _interactedWith = null;
            ReturnToMovement();
            exitInteractable?.Invoke();
            return;
        }

        _currentInteractable.Interact();

        _foundInteractType = _currentInteractable.InteractabeType;

        if (_foundInteractType != InteractState.NonState)
        {
            print("Player entering interact");
            OnCameraOptionFound?.Invoke(_foundCamera);
            RequestStateChange(_states.Interact());
            _interactedWith = _currentInteractable;
            interactedWithSomething?.Invoke();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out Interactable interactable))
        {
            _currentInteractable = interactable;
            _inputObject = other.transform.root.gameObject;
            _foundCamera = interactable.CameraOption;
            nearInteractable.Invoke();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.TryGetComponent(out Interactable interactable))
        {
            if (_interactedWith == interactable)
            {
                _interactedWith = null;
            }

            if (_currentInteractable == interactable)
            {
                _currentInteractable = null;
            }

            if (_foundCamera != null)
            {
                _foundCamera = null;
            }
            walkAwayInteractable.Invoke();
        }
    }
}