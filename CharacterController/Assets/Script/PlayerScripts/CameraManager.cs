using System.Collections;
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
    public static CameraManager Instance;


    public CinemachineBrain brain;
    public CinemachineCamera playerCamera;
    public float blendDuration = 1f;
    CinemachineCamera activeCamera;

    int defaultPriority;

    public UnityEvent RevertToPlayerCam;
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(Instance);
        }

        brain = FindAnyObjectByType<CinemachineBrain>();
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

        if (activeCamera == cam || cam == null)
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
    /// <summary>
    /// 
    /// </summary>
    /// <param name="cam"></param>
    /// <param name="duration"></param>
    public void SwitchCamera(CinemachineCamera cam, float duration)
    {
        //now how do we have the camera wait before automatically switching back
        if (brain.IsBlending || cam == null) { return; }

        if (revertRoutine != null)
        {
            StopCoroutine(revertRoutine);
            revertRoutine = null;
        }

        SwitchCamera(cam);
        revertRoutine = StartCoroutine(RevertAfterDelay(duration));

    }
    private Coroutine revertRoutine;
    private IEnumerator RevertAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        RevertToPlayerCamera();
        revertRoutine = null;
    }
    public void RevertToPlayerCamera()
    {
        if (activeCamera != null)
        {
            activeCamera.Priority = defaultPriority - 1;
            activeCamera = null;
            RevertToPlayerCam?.Invoke();
        }
    }
}
