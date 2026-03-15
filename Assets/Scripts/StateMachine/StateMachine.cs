using UnityEngine;

public class StateMachine 
{
    public State CurrentState {  get; private set; }

    public void Initialize(State startingState)
    {
        CurrentState = startingState;
        CurrentState.Enter();
    }
    public void ChangeState(State state)
    {
        CurrentState?.Exit();
        CurrentState = state;
        CurrentState.Enter();
    }
}
