using System.Collections.Generic;
using Unity.Cinemachine;
using UnityEngine;
using UnityEngine.Events;

//[System.Serializable]
public class Interactable : MonoBehaviour
{
    [Header("Interactable Fields")]
    [SerializeField] private List<GameObject> targets;
    [SerializeField] private UnityEvent OnInteract;

    [Tooltip("Optional, but on interact the player will switch to this camera")]
    [SerializeField] private CinemachineCamera _cameraOption;

    [Tooltip("Player will read this to determine what state it becomes")]
    [SerializeField] private InteractState _interactableType;

    //private bool isPressed;
    [Tooltip("Whenever pressed it will activate the OnInteract.Event")]
    public bool _alwaysTrigger;

    [Space(20)]
    private bool _isPressed;

    public InteractState InteractabeType { get { return _interactableType; } }
    public CinemachineCamera CameraOption { get { return _cameraOption; } }

    [ContextMenu("Interact")]
    public void Interact()
    {
        if (targets != null && !_isPressed || _alwaysTrigger)
        {
            OnInteract.Invoke();
        }
        //print("I was interacted with!");
        _isPressed = !_isPressed;
    }
}
