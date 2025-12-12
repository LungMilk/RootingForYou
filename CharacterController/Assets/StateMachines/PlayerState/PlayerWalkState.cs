using UnityEngine;

public class PlayerWalkState : PlayerBaseState
{
    public PlayerWalkState(PlayerStateMachine currentContext, playerStateFactory playerStateFactory) : base(currentContext, playerStateFactory) { }
    public override void EnterState() { }
    public override void UpdateState() { CheckSwitchStates(); }
    public override void ExitState() { }
    public override void CheckSwitchStates() { 
    if (!Ctx.IsMovementPressed)
        {
            SwitchState(Factory.Idle());
        }else if (Ctx.IsMovementPressed && Ctx.IsRunPressed)
        {
            SwitchState(Factory.Run());
        }
    }
    public override void InitializeSubState() { }
}
