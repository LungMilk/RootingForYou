using UnityEngine;
using UnityEngine.InputSystem;

public class ThirdPersonCameraController : MonoBehaviour
{
    [SerializeField] private PlayerStateMachine playerStateMachine;
    [SerializeField] private Transform cameraYawTarget;
    [SerializeField] private Transform cameraPitchTarget;

    [Header("Look Settings")]
    [SerializeField] private float mouseSensitivity = 0.1f;
    [SerializeField] private float minPitch = -35f;
    [SerializeField] private float maxPitch = 70f;

    private float yaw;
    private float pitch;

    private void Start()
    {
        if (playerStateMachine == null)
        {
            playerStateMachine = GetComponent<PlayerStateMachine>();
        }

        if (cameraYawTarget != null)
        {
            yaw = cameraYawTarget.eulerAngles.y;
        }

        if (cameraPitchTarget != null)
        {
            pitch = cameraPitchTarget.localEulerAngles.x;
            if (pitch > 180f)
            {
                pitch -= 360f;
            }
        }
    }

    private void Update()
    {
        if (playerStateMachine == null || cameraYawTarget == null || cameraPitchTarget == null)
            return;

        Vector2 lookInput = playerStateMachine.CurrentLookInput;

        yaw += lookInput.x * mouseSensitivity;
        pitch -= lookInput.y * mouseSensitivity;
        pitch = Mathf.Clamp(pitch, minPitch, maxPitch);

        cameraYawTarget.rotation = Quaternion.Euler(0f, yaw, 0f);
        cameraPitchTarget.localRotation = Quaternion.Euler(pitch, 0f, 0f);
    }
}