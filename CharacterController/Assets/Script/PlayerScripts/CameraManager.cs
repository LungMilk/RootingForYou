using Unity.Cinemachine;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;

[System.Serializable]
public class CameraEvent: UnityEvent<CinemachineCamera>
{

}
public class CameraManager : MonoBehaviour
{
    //this needs to, when interacting with an object that has a camera, set the current active camera to that camera.
    //then we need to, when no longer interacting (button press again) return to the default player camera.

    public CinemachineBrain brain;
    public CinemachineCamera playerCamera;
    public float blendDuration = 1f;
    CinemachineCamera activeCamera;

    int defaultPriority;

    private void Awake()
    {
        defaultPriority = playerCamera.Priority;
        brain.DefaultBlend.Time = blendDuration;
    }
    //how can a player then interact with the object then if interact was called, send the found camera to the camera manager.
    public void SwitchCamera(CinemachineCamera cam)
    {
        //the switch needs to switch to a different camera each time, but how can it switch back when calling the same function??
        //if it is called and it is the same camera then we just go back to default cam?
        //let priority take over??
        if (brain.IsBlending) { return; }

        if (activeCamera == cam)
        {
            RevertToPlayerCamera();
            return;
        }

        if (activeCamera != null)
        {
            activeCamera.Priority = defaultPriority - 1;
        }

        cam.Priority = defaultPriority + 1;
        activeCamera = cam;
    }

    private void RevertToPlayerCamera()
    {
        if (activeCamera != null)
        {
            activeCamera.Priority = defaultPriority - 1;
            activeCamera = null;
        }
    }
}
