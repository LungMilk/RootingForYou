using System.Threading.Tasks;
using Unity.Cinemachine;
using Unity.VisualScripting;
using Unity.VisualScripting.FullSerializer;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;

public class PlayerWalkState : PlayerBaseState
{
    //sound effects
    float footstepSpeedSeconds = 0.5f;
    float currentTime = 0f;

    
    public PlayerWalkState(PlayerStateMachine currentContext, playerStateFactory playerStateFactory) : base(currentContext, playerStateFactory) { }
    public override void EnterState() {
        Ctx._animator.SetBool("isWalking", true);
       
    }
    public override void UpdateState()
    {
        HandleRotation();
        Vector2 input = Ctx.CurrentMovementInput;
        Vector3 forward = Ctx.transform.forward;
        Vector3 right = Ctx.transform.right;

        forward.y = 0f;
        right.y = 0f;
        forward.Normalize();
        right.Normalize();

        Vector3 moveDirection = (forward * input.y + right * input.x).normalized;
        Ctx.CurrentMovement = moveDirection;
        Ctx._chrController.Move(moveDirection * Ctx._movementSpeed * Time.deltaTime);
        CheckSwitchStates();

        currentTime += Time.deltaTime;

        if (currentTime >= footstepSpeedSeconds)
        {
            SoundEffectManager.Play("Footsteps");
            currentTime = 0f;
        }
        
    }
    public override void ExitState() 
    {
        //stop animation
        Ctx._animator.SetBool("isWalking", false);
    }
    public override void CheckSwitchStates() { 
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
    void HandleRotation()
    {
        //Vector3 positionToLookAt;
        //positionToLookAt.x = Ctx.CurrentMovementInput.x;
        //positionToLookAt.y = 0f;
        //positionToLookAt.z = Ctx.CurrentMovement.z;

        //if (Ctx.IsMovementPressed)
        //{
        //    Quaternion targetRotaion = Quaternion.LookRotation(positionToLookAt);
        //    Ctx.transform.rotation = Quaternion.Slerp(Ctx.transform.rotation, targetRotaion, Ctx._rotationFactorPerFrame * Time.deltaTime);
        //}
        Vector3 moveDir = new Vector3(Ctx.CurrentMovement.x, 0f, Ctx.CurrentMovement.z);
        Debug.DrawRay(
        Ctx.transform.position,
        moveDir,
        Color.red
        );
        if (moveDir.sqrMagnitude > 0.01f)
        {
            Quaternion targetRotation = Quaternion.LookRotation(moveDir);
            Ctx.transform.rotation = Quaternion.Slerp(Ctx.transform.rotation, targetRotation, Ctx._rotationFactorPerFrame * Time.deltaTime);
        }
    }
}
