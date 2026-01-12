using Unity.VisualScripting.FullSerializer;
using UnityEngine;
using System.Collections.Generic;

public class PlayerIdleState : PlayerBaseState
{
    public PlayerIdleState(PlayerStateMachine currentContext, playerStateFactory playerStateFactory) : base(currentContext, playerStateFactory) { }

    public override void EnterState() { }
    public override void UpdateState() { CheckSwitchStates(); }
    public override void ExitState() { }
    public override void CheckSwitchStates() 
    {
        //Debug.Log("Idle check running");
        //SwitchState(Factory.Walk());
        if (Ctx.IsMovementPressed)
        {
            SwitchState(Factory.Walk());
        }
        if (Ctx.IsInteracting)
        {
            SwitchState(Factory.Interact());
        }
    
    }
    public override void InitializeSubState() { }
}
