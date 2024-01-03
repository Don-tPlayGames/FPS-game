using System;
using System.Collections.Generic;
using FPSgame.Scripts.Base.Utils;
using Godot;

namespace FPSgame.Scripts.Node.Character.State;

public partial class CharacterStateMachine : Godot.Node, IStateContext<AbstractCharacterState>
{
    [Export] private NodePath _characterController;

    [Export] private NodePath _defaultState;

    public AbstractCharacterState CurrentState { get; protected set; }
    
    public CharacterBasic Character { get; protected set; }
    
    public ICharacterController CharacterController { get; protected set; }

    protected Dictionary<Type, AbstractCharacterState> States;
    
    public AbstractCharacterState DefaultState { get; protected set; }

    public override void _Ready()
    {
        Character = GetParent<CharacterBasic>();
        CharacterController = GetNode<ICharacterController>(_characterController);

        GatherStates();

        Character.OnReady += OnCharacterReady;
    }

    protected virtual void GatherStates()
    {
        States = new Dictionary<Type, AbstractCharacterState>();
        foreach (var node in GetChildren())
        {
            if (node is not AbstractCharacterState state) continue;
            
            States[state.GetType()] = state;
        }

        AbstractCharacterState defaultState = GetNodeOrNull<AbstractCharacterState>(_defaultState);
        if (!(defaultState?.GetParentOrNull<CharacterStateMachine>()?.Equals(this) ?? false))
            throw new Exception($"Specified initial state '{_defaultState}' is not a part of the {GetPath()}.");
        
        DefaultState = defaultState;
    }

    protected virtual void OnCharacterReady()
    {
        SetupInitialState();
    }

    public override void _Process(double deltaTime)
    {
        CurrentState.Update(deltaTime);
    }

    public override void _PhysicsProcess(double deltaTime)
    {
        CurrentState.PhysicsUpdate(deltaTime);
    }

    public virtual void SwitchState(AbstractCharacterState newState)
    {
        if (!newState.IsRootState) return;
        
        CurrentState.Exit();
        CurrentState = newState;
        CurrentState.Enter();
    }

    public virtual bool SwitchState(Type key)
    {
        if (TryGetState(key, out AbstractCharacterState state))
        {
            SwitchState(state);
            return true;
        }

        GD.PrintErr($"Character FSM has no state of type: {key}");
        return false;
    }

    public bool TryGetState(Type key, out AbstractCharacterState state)
    {
        return States.TryGetValue(key, out state);
    }

    protected virtual void SetupInitialState()
    {
        CurrentState = DefaultState;
        CurrentState.Enter();
    }
}