public class StateMachine
{
    private StateBase currentState;

    public StateMachine(StateBase initState)
    {
        currentState = initState;
        currentState.OnEnterState();
    }

    public void UpdateState()
    {
        currentState?.OnUpdate();
    }

    public void ChangeState(StateBase nextState)
    {
        currentState?.OnExitState();
        currentState = nextState;
        currentState.OnEnterState();
    }
}