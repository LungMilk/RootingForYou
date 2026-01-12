using UnityEngine;

public class PlayerDialogueState : PlayerBaseState
{
    float timer = 0f;
    public PlayerDialogueState(PlayerStateMachine currentContext, playerStateFactory playerStateFactory) : base(currentContext, playerStateFactory) { }

    public override void EnterState() { timer = 0f; }
    public override void UpdateState() {
        if (Ctx.IsInteractPressed)
        {
            //progress dialogue??
            Debug.Log("talking");
        }
        CheckSwitchStates(); }
    public override void ExitState() { }
    public override void CheckSwitchStates()
    {
        //dialogue can only finish if dialogue is over
        timer += Time.deltaTime;
        //if (Ctx.IsInteractPressed)
        //{
        //    SetSuperState(Factory.Movement());
        //}else 
        if(timer > 10f)
        {
            Ctx.ForceSuperState(Factory.Movement());
        }
        
    }
    public override void InitializeSubState() { }
}
