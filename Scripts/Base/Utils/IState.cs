namespace FPSgame.Scripts.Base.Utils;

public interface IState<out TStateMachine, in TState, in TStateKey>
    where TStateMachine : IStateMachine<TStateMachine, TState, TStateKey>
    where TState : IState<TStateMachine, TState, TStateKey>
{
    TStateMachine StateMachine { get; }
    
    void Enter() {}
    void Exit() {}
    
    void Update(double deltaTime) {}
    void PhysicsUpdate(double deltaTime) {}
}