using UnityEngine;

public class StateMachine 
{
    public State CurrentState {  get; private set; }

    public void Initialize(State startingState)
    {
        CurrentState = startingState;
        CurrentState.Enter();
    }
    /// <summary>
    /// Не использовать прямо через ссылку на машину состояний. Ищи реализацию для Dictionary в классе используеющем StateMachine
    /// </summary>
    public void ChangeState(State state) // НЕ РАБОТАЙ С ЭТИМ МЕТОДОМ, ИСПОЛЬЗУЙ ChangeState через джинерик в Enemy
    {
        CurrentState?.Exit();
        CurrentState = state;
        CurrentState.Enter();
    }
    public void Update()
    {
        CurrentState?.Update();
    }
}
