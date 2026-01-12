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
            {typeof(PlayerIdleState), () => Grounded() },
            {typeof(PlayerWalkState), () => Grounded() },
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
    public PlayerBaseState Grounded()
    {
        print("Grounded");
        return new PlayerGroundedState(_context, this);
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
    
}
