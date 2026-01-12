using UnityEngine;

public class PlayerInteractState : PlayerBaseState
{
    //handle gravity does not really exist
    public PlayerInteractState(PlayerStateMachine currentContext, playerStateFactory playerStateFactory) : base(currentContext, playerStateFactory)
    {
        IsRootState = true;
    }
    public override void EnterState()
    {
        //_currentMovement.y = groundedGravity;
        //_appliedMovement.y = _groundedGravity;
        InitializeSubState();
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.Confined;
    }
    public override void UpdateState() { CheckSwitchStates(); }
    public override void ExitState() { }
    public override void CheckSwitchStates() { }
    public override void InitializeSubState()
    {
        //Debug.Log("Superstate test"+ Ctx.IsMovementPressed);
        // vDebug.Log("states initialized");
        if(Ctx.FoundInteractType == InteractState.Dialogue)
        {
            SetSubState(Factory.Dialogue());
        }
        else if (Ctx.FoundInteractType == InteractState.Planting)
        {
            SetSubState(Factory.Planting());
        }
    }
}
