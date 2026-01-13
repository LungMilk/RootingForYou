using Unity.VisualScripting.FullSerializer;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerWalkState : PlayerBaseState
{
    public PlayerWalkState(PlayerStateMachine currentContext, playerStateFactory playerStateFactory) : base(currentContext, playerStateFactory) { }
    public override void EnterState() { }
    public override void UpdateState()
    { //Debug.Log(_currentMovement);
        //Debug.Log(Ctx.IsMovementPressed);
        Vector2 input = Ctx.CurrentMovementInput;
        //Debug.Log(input);
        Ctx.CurrentMovement = new Vector3(input.x, Ctx.CurrentMovement.y, input.y);

        //chrController.Move(currentMovement);
        //look into simple move
        Ctx._chrController.Move(Ctx.CurrentMovement * Ctx._movementSpeed * Time.deltaTime);

        HandleRotation();
        CheckSwitchStates(); 
    }
    public override void ExitState() { }
    public override void CheckSwitchStates() { 
        if (!Ctx.IsMovementPressed)
        {
            SwitchState(Factory.Idle());
        }
        if (Ctx.InteractPressedThisFrame)
        {
            SwitchState(Factory.Interact());
        }
    }
    public override void InitializeSubState() { }
    void HandleRotation()
    {
        Vector3 positionTolookAt;
        positionTolookAt.x = Ctx.CurrentMovement.x;
        positionTolookAt.y = 0.0f;
        positionTolookAt.z = Ctx.CurrentMovement.z;

        //Quaternion currentRotation = Ctx.transform.rotation;
        //Quaternion targetRotation = Quaternion.LookRotation(positionTolookAt);

        if (Ctx.IsMovementPressed)
        {
            Quaternion targetRotation = Quaternion.LookRotation(positionTolookAt);
            Ctx.transform.rotation = Quaternion.Slerp(Ctx.transform.rotation, targetRotation, Ctx._rotationFactorPerFrame * Time.deltaTime);
        }
    }
}
