using Unity.Cinemachine;
using UnityEngine;

public class DebugCameraBox : MonoBehaviour,IInteractable, ICameraOption
{
    [SerializeField] private CinemachineCamera cameraOption;
    public CinemachineCamera CameraOption
    {
        get => cameraOption;
        set => cameraOption = value;
    }

    public void Interact()
    {
        print("Switch to my camera!!");
    }
}
