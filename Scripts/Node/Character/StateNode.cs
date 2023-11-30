using FPSgame.Scripts.Base.Utils;

namespace FPSgame.Scripts.Node.Character;

public abstract partial class StateNode<TStateMachine> : Godot.Node, IState<TStateMachine, StateNode<TStateMachine>, string>
    where TStateMachine : IStateMachine<TStateMachine, StateNode<TStateMachine>, string>
{
    public TStateMachine StateMachine { get; private set; }

    public virtual void Enter() {}
    public virtual void Exit() {}

    public virtual void Init(TStateMachine parent)
    {
        StateMachine = parent;
    }
    public virtual void Update(double deltaTime) {}
    public virtual void PhysicsUpdate(double deltaTime) {}
}