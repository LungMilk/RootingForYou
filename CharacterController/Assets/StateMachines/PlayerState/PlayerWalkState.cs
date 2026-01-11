using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerWalkState : PlayerBaseState
{
    public PlayerWalkState(PlayerStateMachine currentContext, playerStateFactory playerStateFactory) : base(currentContext, playerStateFactory) { }
    public override void EnterState() { }
    public override void UpdateState()
    { //Debug.Log(_currentMovement);
        HandleRotation();
        //chrController.Move(currentMovement);
        //look into simple move
        Ctx._chrController.Move(Ctx.CurrentMovement * Ctx._movementSpeed * Time.deltaTime);
        CheckSwitchStates(); 
    }
    public override void ExitState() { }
    public override void CheckSwitchStates() { 
    if (!Ctx.IsMovementPressed)
        {
            SwitchState(Factory.Idle());
        }
    }
    public override void InitializeSubState() { }
    void HandleRotation()
    {
        Vector3 positionTolookAt;
        positionTolookAt.x = Ctx.CurrentMovement.x;
        positionTolookAt.y = 0.0f;
        positionTolookAt.z = Ctx.CurrentMovement.z;

        Quaternion currentRotation = Ctx.transform.rotation;
        //Quaternion targetRotation = Quaternion.LookRotation(positionTolookAt);

        if (Ctx.IsMovementPressed)
        {
            Quaternion targetRotation = Quaternion.LookRotation(positionTolookAt);
            Ctx.transform.rotation = Quaternion.Slerp(currentRotation, targetRotation, Ctx._rotationFactorPerFrame * Time.deltaTime);
        }
    }
}
