using System;
using System.Collections.Generic;
using UnityEngine;

public class playerStateFactory : MonoBehaviour
{
    private PlayerStateMachine _context;

    private Dictionary<Type, Func<PlayerBaseState>> _substateToSuperState;

    public playerStateFactory(PlayerStateMachine currentContext)
    {
        _context = currentContext;

        _substateToSuperState = new Dictionary<Type, Func<PlayerBaseState>>
        {
            { typeof(PlayerIdleState), () => Movement() },
            { typeof(PlayerWalkState), () => Movement() },
        };
    }

    public PlayerBaseState GetSuperstateForSubState(PlayerBaseState substate)
    {
        var type = substate.GetType();
        if (_substateToSuperState.ContainsKey(type))
        {
            return _substateToSuperState[type]();
        }

        UnityEngine.Debug.Log("No superstate found for: " + substate);
        return null;
    }

    public PlayerBaseState Movement()
    {
        return new PlayerMovementState(_context, this);
    }

    public PlayerBaseState Interact()
    {
        return new PlayerInteractState(_context, this);
    }

    public PlayerBaseState Idle()
    {
        return new PlayerIdleState(_context, this);
    }

    public PlayerBaseState Walk()
    {
        return new PlayerWalkState(_context, this);
    }

    public PlayerBaseState Dialogue()
    {
        return new PlayerDialogueState(_context, this);
    }

    public PlayerBaseState Planting()
    {
        return new PlayerPlantingState(_context, this);
    }
}