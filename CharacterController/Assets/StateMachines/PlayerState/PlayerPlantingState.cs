using Unity.VisualScripting.FullSerializer;
using UnityEngine;

public class PlayerPlantingState : PlayerBaseState
{
    public PlayerPlantingState(PlayerStateMachine currentContext, playerStateFactory playerStateFactory) : base(currentContext, playerStateFactory) { }

    public override void EnterState() {
    }
    public override void UpdateState() { 
        if (Input.GetMouseButtonDown(0))
        {
            Debug.Log("planted a plant");
        }
        
        CheckSwitchStates(); }
    public override void ExitState() {
       
    }
    public override void CheckSwitchStates()
    {
        if (Ctx.CanSwitchSupers)
        {
            Ctx.ForceSuperState(Factory.Movement());
        }
    }
    public override void InitializeSubState() { }
}
