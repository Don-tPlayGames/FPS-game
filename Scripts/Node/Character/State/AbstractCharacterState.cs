using System;
using FPSgame.Scripts.Base.Utils;
using Godot;

namespace FPSgame.Scripts.Node.Character.State;

public abstract partial class AbstractCharacterState : Godot.Node, IStateState
{
    [Export] public bool IsRootState { get; protected set; }
    
    protected CharacterStateMachine Context { get; private set; }

    protected AbstractCharacterState CurrentSuperState { get; private set; }
    protected AbstractCharacterState CurrentSubState { get; private set; }

    public override void _Ready()
    {
        Context = GetParent<CharacterStateMachine>();
    }

    public virtual void Enter() { }

    public virtual void Exit()
    {
        CurrentSubState?.Exit();
    }

    public virtual void Update(double deltaTime)
    {
        UpdateSubStates(deltaTime);
    }

    public virtual void PhysicsUpdate(double deltaTime)
    {
        PhysicsUpdateSubStates(deltaTime);
    }
    
    protected virtual void UpdateSubStates(double deltaTime)
    {
        CurrentSubState?.Update(deltaTime);
    }

    protected virtual void PhysicsUpdateSubStates(double deltaTime)
    {
        CurrentSubState?.PhysicsUpdate(deltaTime);
    }

    private void SetSuperState(AbstractCharacterState newSuperState)
    {
        if(IsRootState) return;
        
        CurrentSuperState = newSuperState;
    }

    protected void SwitchSubState(AbstractCharacterState newSubState)
    {
        CurrentSubState?.Exit();
        CurrentSubState = newSubState;

        if (CurrentSubState is null) return;
        
        CurrentSubState.Enter();
        CurrentSubState.SetSuperState(this);
    }

    public bool SwitchSubState(Type key)
    {
        if (Context.TryGetState(key, out AbstractCharacterState state))
        {
            SwitchSubState(state);
            return true;
        }

        return false;
    }
}