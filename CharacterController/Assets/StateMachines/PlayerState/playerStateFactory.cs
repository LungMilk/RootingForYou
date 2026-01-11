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
        print("Idle");
        return new PlayerIdleState(_context, this);
    }
    public PlayerBaseState Walk()
    {
        print("Walk");
        return new PlayerWalkState(_context,this);
    }
    public PlayerBaseState Grounded()
    {
        print("Grounded");
        return new PlayerGroundedState(_context, this);
    }
    
}
