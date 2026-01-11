using UnityEngine;

public class PlayerRunState : PlayerBaseState
{
    public PlayerRunState(PlayerStateMachine currentContext, playerStateFactory playerStateFactory): base (currentContext,playerStateFactory) { }

    public override void EnterState() { }
    public override void UpdateState() {
        CheckSwitchStates();
    }
    public override void ExitState() { }
    public override void CheckSwitchStates()
    {
        if (!Ctx.IsMovementPressed)
        {
            SwitchState(Factory.Idle());
        }
        else if (Ctx.IsMovementPressed)
        {
            SwitchState(Factory.Walk());
        }
    }
    public override void InitializeSubState() { }
}
