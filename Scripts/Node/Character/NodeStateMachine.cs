using System;
using System.Collections.Generic;
using FPSgame.Scripts.Base.Utils;
using Godot;

namespace FPSgame.Scripts.Node.Character;

public abstract partial class NodeStateMachine<TState> : Godot.Node, IStateMachine<NodeStateMachine<TState>, TState, string>
    where TState : Godot.Node, IState<NodeStateMachine<TState>, TState, string>
{
    [Export] private NodePath InitialState;
    
    private readonly Dictionary<string, TState> _states = new();

    public TState CurrentState { get; private set; }

    public override void _Ready()
    {
        foreach (var node in GetChildren())
        {
            if (node is not TState state) continue;
            
            _states[node.Name] = state;
            InitState(state);
        }

        TState initial = GetNode<TState>(InitialState);
        if (!(initial.GetParentOrNull<NodeStateMachine<TState>>()?.Equals(this) ?? false))
            throw new Exception($"Specified initial state '{InitialState}' is not a part of the '{GetPath()}' StateMachine.");

        CurrentState = initial;
        CurrentState.Enter();
    }

    protected virtual void InitState(TState state)
    {
        //state.Init(this);
        state.Exit();
    }

    public override void _Process(double deltaTime)
    {
        CurrentState.Update(deltaTime);
    }

    public override void _PhysicsProcess(double deltaTime)
    {
        CurrentState.PhysicsUpdate(deltaTime);
    }

    public bool SetState(string stateName)
    {
        if (!_states.TryGetValue(stateName, out var targetState)) return false;

        if (targetState == CurrentState) return false;
        
        CurrentState.Exit();
        CurrentState = targetState;
        CurrentState.Enter();

        return true;
    }
}