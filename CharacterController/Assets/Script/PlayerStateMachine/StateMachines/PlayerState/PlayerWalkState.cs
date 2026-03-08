using ScriptableObjects;
using UnityEngine;

public class PlayerWalkState : PlayerBaseState
{
    private float footstepSpeedSeconds = 0.5f;
    private float currentTime = 0f;
    private SoundEffectSO walkingSound;

    public PlayerWalkState(PlayerStateMachine currentContext, playerStateFactory playerStateFactory)
        : base(currentContext, playerStateFactory) { }

    public override void EnterState()
    {
        if (Ctx._animator != null)
        {
            Ctx._animator.SetBool("isWalking", true);
        }

        if (Ctx._soundEffects != null && Ctx._soundEffects.Count > 0)
        {
            walkingSound = Ctx._soundEffects[0];
        }
    }

    public override void UpdateState()
    {
        HandleMovement();
        CheckSwitchStates();
        HandleFootsteps();
    }

    public override void ExitState()
    {
        if (Ctx._animator != null)
        {
            Ctx._animator.SetBool("isWalking", false);
        }
    }

    public override void CheckSwitchStates()
    {
        if (!Ctx.IsMovementPressed)
        {
            SwitchState(Factory.Idle());
        }

        if (Ctx.InteractPressedThisFrame)
        {
            SwitchState(Factory.Interact());
        }
    }

    public override void InitializeSubState() { }

    private void HandleMovement()
    {
        if (Ctx.CameraTransform == null)
            return;

        Vector2 input = Ctx.CurrentMovementInput;

        Vector3 cameraForward = Ctx.CameraTransform.forward;
        Vector3 cameraRight = Ctx.CameraTransform.right;

        cameraForward.y = 0f;
        cameraRight.y = 0f;

        cameraForward.Normalize();
        cameraRight.Normalize();

        Vector3 moveDirection = cameraForward * input.y + cameraRight * input.x;

        if (moveDirection.sqrMagnitude > 1f)
        {
            moveDirection.Normalize();
        }

        float speed = Ctx.IsRunPressed ? Ctx._runSpeed : Ctx._walkSpeed;

        if (Ctx._chrController.isGrounded)
        {
            if (Ctx.VerticalVelocity < 0f)
            {
                Ctx.VerticalVelocity = Ctx._groundedGravity;
            }
        }
        else
        {
            Ctx.VerticalVelocity += Ctx._gravity * Time.deltaTime;
        }

        Vector3 velocity = moveDirection * speed;
        velocity.y = Ctx.VerticalVelocity;

        Ctx.CurrentMovement = velocity;
        Ctx._chrController.Move(velocity * Time.deltaTime);

        HandleRotation(moveDirection);
    }

    private void HandleRotation(Vector3 moveDirection)
    {
        if (moveDirection.sqrMagnitude <= 0.0001f)
            return;

        Quaternion targetRotation = Quaternion.LookRotation(moveDirection);
        Ctx.transform.rotation = Quaternion.Slerp(
            Ctx.transform.rotation,
            targetRotation,
            Ctx._rotationFactorPerFrame * Time.deltaTime
        );
    }

    private void HandleFootsteps()
    {
        if (walkingSound == null) return;
        if (!Ctx._chrController.isGrounded) return;
        if (!Ctx.IsMovementPressed) return;

        currentTime += Time.deltaTime;

        if (currentTime >= footstepSpeedSeconds)
        {
            walkingSound.Play();
            currentTime = 0f;
        }
    }
}