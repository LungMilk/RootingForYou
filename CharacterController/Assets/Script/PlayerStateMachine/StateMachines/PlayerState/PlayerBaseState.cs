
using Unity.VisualScripting;
using Unity.VisualScripting.FullSerializer;

public abstract class PlayerBaseState
{
    private bool _isRootState = false;
    private PlayerStateMachine _ctx;
    private playerStateFactory _factory;
    private PlayerBaseState _currentSubState;
    private PlayerBaseState _currentSuperState;

    protected bool IsRootState { set { _isRootState = value; } }
    protected PlayerStateMachine Ctx { get { return _ctx; } }
    protected playerStateFactory Factory {get {return _factory;}}

    public PlayerBaseState(PlayerStateMachine currentContext, playerStateFactory playerStateFactory)
    {
        _ctx = currentContext;
        _factory = playerStateFactory;
    }

    //establish methods every state will have
    public abstract void EnterState();
    public abstract void UpdateState();
    public abstract void ExitState();

    protected void ExitSubState()
    {
        if (_currentSubState != null)
        {
            _currentSubState.ExitSubState();
            _currentSubState.ExitState();
            _currentSubState = null;
        }
    }
    public abstract void CheckSwitchStates();
    public abstract void InitializeSubState();

    public void UpdateStates()
    {
        UpdateState();
        if(_currentSubState != null)
        {
            _currentSubState.UpdateStates();
        }
    }
    protected void SwitchState(PlayerBaseState newState) {
        ExitSubState();
        ExitState();

        newState.EnterState();

        if (_isRootState)
        {
            _ctx.CurrentState = newState;
        } else if (_currentSuperState != null)
        {
            _currentSuperState.SetSubState(newState);
        }
    }
    protected void SetSuperState(PlayerBaseState newSuperState) { _currentSuperState = newSuperState; }
    protected void SetSubState(PlayerBaseState newSubState) {

        if(_currentSubState != null)
        {
            _currentSubState.ExitState();
            _currentSubState.ExitSubState();
        }
        //_currentSubState?.ExitState();
        _currentSubState = newSubState;
        newSubState.SetSuperState(this);
        newSubState.EnterState();
    }
}
