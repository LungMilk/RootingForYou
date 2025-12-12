using UnityEngine;

public class PlayerGroundedState : PlayerBaseState
{
    //handle gravity does not really exist
    public PlayerGroundedState(PlayerStateMachine currentContext, playerStateFactory playerStateFactory) : base(currentContext, playerStateFactory) 
    {
        IsRootState = true;
        InitializeSubState();
    }
    public override void EnterState() {
        //_currentMovement.y = groundedGravity;
        //_appliedMovement.y = _groundedGravity;
    }
    public override void UpdateState() { CheckSwitchStates(); }
    public override void ExitState() { }
    public override void CheckSwitchStates() { }
    public override void InitializeSubState() {
        if (!Ctx.IsMovementPressed && !Ctx.IsRunPressed) {
            SetSubState(Factory.Idle());
        }else if (Ctx.IsMovementPressed && !Ctx.IsRunPressed){
            SetSubState(Factory.Walk());
        }
        else
        {
            SetSubState(Factory.Run());
        }
        
        }
}
