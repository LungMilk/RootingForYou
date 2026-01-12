using System;
using System.Collections.Generic;
using UnityEngine;

public class playerStateFactory : MonoBehaviour
{
    PlayerStateMachine _context;

    private Dictionary<Type, Func<PlayerBaseState>> _substateToSuperState;
    public playerStateFactory(PlayerStateMachine currentContext)
    {
        _context = currentContext;

        _substateToSuperState = new Dictionary<Type, Func<PlayerBaseState>>
        {
            //{typeof(PlayerDialogueState), => Interact() },
            {typeof(PlayerIdleState), () => Movement() },
            {typeof(PlayerWalkState), () => Movement() },
        };
    }
    public PlayerBaseState GetSuperstateForSubState(PlayerBaseState substate)
    {
        var type = substate.GetType();
        if (_substateToSuperState.ContainsKey(type))
        {
            return _substateToSuperState[type]();
        }
        print("no Superstate found for: "+ substate.ToString());
        return null;
    }

    //super states
    public PlayerBaseState Movement()
    {
        print("Movement");
        return new PlayerMovementState(_context, this);
    }

    public PlayerBaseState Interact()
    {
        print("Interact");
        return new PlayerInteractState(_context, this);
    }

    //substates
    public PlayerBaseState Idle()
    {
        print("Idle");
        return new PlayerIdleState(_context, this);
    }
    public PlayerBaseState Walk()
    {
        print("Walk");
        return new PlayerWalkState(_context,this);
    }

    public PlayerBaseState Dialogue()
    {
        print("Dialogue");
        return new PlayerDialogueState(_context, this);
    }
    public PlayerBaseState Planting()
    {
        print("Planting");
        return new PlayerPlantingState(_context, this);
    }

}
