using System;
using Unity.Cinemachine;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

public class PlayerInteractions : MonoBehaviour
{
    //3d interactions based off of the interfaces and the like, just run with it for now

    //we may need an interactable priority system but right now it gets the nearest in the collision
    //we save the interactable as doing a call for getting any on the button press sounds pretty taxing.
    public GameObject inputObject;
    PlayerInputs _playerInput;
    public bool _isInteractPressed;
    private InputAction _interactAction;
    IInteractable _currentInteractable;

    public CameraEvent OnCameraOptionFound;
    private CinemachineCamera foundCamera;

    private void OnEnable()
    {
        //we will fix in the player state machine when we get to It I need a prototype
        _playerInput = inputObject.GetComponent<PlayerInputHandler>().playerInputHandler;
        //cool shorthand instead of long things seen earlier
        _interactAction = _playerInput.CharacterControls.Interact;

        _interactAction.started += OnInteract;
        _interactAction.canceled += OnInteract;
    }

    void Update()
    {
        //_isInteractPressed = context.ReadValueAsButton();
        //print("Hi");
        if (_currentInteractable != null && _isInteractPressed)
        {
            print("trying to interact");
            _currentInteractable.Interact();
            OnCameraOptionFound.Invoke(foundCamera);
        }

    }
    //probs not important
    void OnInteract(InputAction.CallbackContext context)
    {
        _isInteractPressed = context.ReadValueAsButton();
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out IInteractable interactable))
        {
            _currentInteractable = interactable;
        }
        if (other.TryGetComponent(out ICameraOption cameraOption))
        {
            foundCamera = cameraOption.CameraOption;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.TryGetComponent(out IInteractable interactable))
        {
            if (_currentInteractable == interactable)
            {
                _currentInteractable = null;
            }
            if (foundCamera != null)
            {
                foundCamera = null;
            }
        }
    }
}
