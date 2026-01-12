using UnityEngine;

public class PlayerMovementState : PlayerBaseState
{
    //handle gravity does not really exist
    public PlayerMovementState(PlayerStateMachine currentContext, playerStateFactory playerStateFactory) : base(currentContext, playerStateFactory)
    {
        IsRootState = true;
    }
    public override void EnterState()
    {
        //_currentMovement.y = groundedGravity;
        //_appliedMovement.y = _groundedGravity;
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;

        Ctx.OnCameraOptionFound.Invoke(null);
        InitializeSubState();
    }
    public override void UpdateState() { HandleGravity(); CheckSwitchStates(); }
    public override void ExitState() { }
    public override void CheckSwitchStates() { }
    public override void InitializeSubState()
    {
        //Debug.Log("Superstate test"+ Ctx.IsMovementPressed);
        // vDebug.Log("states initialized");
        if (!Ctx.IsMovementPressed)
        {
            SetSubState(Factory.Idle());
        }
        else if (Ctx.IsMovementPressed)
        {
            SetSubState(Factory.Walk());
        }
    }

    void HandleGravity()
    {
        if (Ctx._chrController.isGrounded)
        {
            float groundedGravity = -.05f;
            Ctx.CurrentMovement.Set(Ctx.CurrentMovement.x, groundedGravity, Ctx.CurrentMovement.z);
        }
        else
        {
            float gravity = -9.8f;
            float y = Ctx.CurrentMovement.y;
            Ctx.CurrentMovement.Set(Ctx.CurrentMovement.x, y += gravity, Ctx.CurrentMovement.z);
            //Ctx.CurrentMovement.y += gravity;
        }
    }
}
