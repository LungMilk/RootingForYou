using Unity.Cinemachine;
using UnityEngine;

public class DebugCameraBox : MonoBehaviour, ICameraOption
{
    public InteractState _interactType;
    public InteractState InteractableType
    {
        get => _interactType; 
        set => _interactType = value;
    }

    [SerializeField] private CinemachineCamera cameraOption;
    public CinemachineCamera CameraOption
    {
        get => cameraOption;
        set => cameraOption = value;
    }

    public void Interact()
    {
        //print("Switch to my camera!!");
    }
}
