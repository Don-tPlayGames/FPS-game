namespace FPSgame.Scripts.Base.Utils;

public interface IStateMachine<in TStateMachine, out TState, in TStateKey>
    where TStateMachine : IStateMachine<TStateMachine, TState, TStateKey>
    where TState : IState<TStateMachine, TState, TStateKey>
{
    TState CurrentState { get; }
    bool SetState(TStateKey stateKey);
}