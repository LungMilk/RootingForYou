using Unity.Cinemachine;
using Unity.VisualScripting.FullSerializer;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;

public class PlayerWalkState : PlayerBaseState
{
    public PlayerWalkState(PlayerStateMachine currentContext, playerStateFactory playerStateFactory) : base(currentContext, playerStateFactory) { }
    public override void EnterState() { }
    public override void UpdateState()
    {
        HandleRotation();
        //Debug.Log(_currentMovement);
        //Debug.Log(Ctx.IsMovementPressed);
        Vector2 input = Ctx.CurrentMovementInput;
        //Debug.Log(input);
        //we need to convert input somehow to local rotation
        Ctx.CurrentMovement = new Vector3(input.x, Ctx.CurrentMovement.y, input.y).normalized;
        Vector3 moveDirection = Ctx.CurrentMovement;

        //Vector3 adjustedDirection = Quaternion.AngleAxis(Ctx._foundCamera.eulerangles.y, Vector3.right.up) * Ctx.CurrentMovement;

        Debug.DrawRay(Ctx.transform.position, Ctx.transform.forward,Color.red);

        //chrController.Move(currentMovement);
        //look into simple move
        Ctx._chrController.Move(moveDirection * Ctx._movementSpeed * Time.deltaTime);
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
