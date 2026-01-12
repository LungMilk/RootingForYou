using Unity.VisualScripting.FullSerializer;
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
            //Debug.Log("talking");
        }
        timer += Time.deltaTime;
        CheckSwitchStates(); }
    public override void ExitState()
    {
        Debug.Log("exiting Dialogue");
        //Ctx.OnCameraOptionFound.Invoke(Ctx._foundCamera);
    }
    public override void CheckSwitchStates()
    {
        //dialogue can only finish if dialogue is over
        //if (Ctx.IsInteractPressed)
        //{
        //    SetSuperState(Factory.Movement());
        //}else 
        if(timer > 5f)
        {
            Ctx.ForceSuperState(Factory.Movement());
        }
        
    }
    public override void InitializeSubState() { }
}
