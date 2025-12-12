using UnityEngine;

public class playerStateFactory : MonoBehaviour
{
    PlayerStateMachine _context;

    public playerStateFactory(PlayerStateMachine currentContext)
    {
        _context = currentContext;
    }
    public PlayerBaseState Idle()
    {
        return new PlayerIdleState(_context, this);
    }
    public PlayerBaseState Walk()
    {
        return new PlayerWalkState(_context,this);
    }
    public PlayerBaseState Run()
    {
        return new PlayerRunState(_context, this);
    }
    public PlayerBaseState Grounded()
    {
        return new PlayerGroundedState(_context, this);
    }
    
}
