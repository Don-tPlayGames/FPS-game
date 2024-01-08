using System;

namespace FPSgame.Scripts.Node.Character.State;

public abstract partial class CharacterStatePoseBasic : CharacterStatePoseAbstract
{
    protected Type LastSubState = typeof(CharacterStateMovementIdle);
    
    public override void Enter()
    {
        base.Enter();
        
        Context.CharacterController.OnSwitchCrouch += OnSwitchCrouch;
        Context.CharacterController.OnSwitchCrawl += OnSwitchCrawl;
        Context.CharacterController.OnJump += OnJump;
        
        SwitchSubState(LastSubState);
    }

    public override void Exit()
    {
        LastSubState = CurrentSubState?.GetType() ?? typeof(CharacterStateMovementIdle);
        
        base.Exit();
        
        Context.CharacterController.OnSwitchCrouch -= OnSwitchCrouch;
        Context.CharacterController.OnSwitchCrawl -= OnSwitchCrawl;
        Context.CharacterController.OnJump -= OnJump;
    }

    protected abstract void OnSwitchCrouch();

    protected abstract void OnSwitchCrawl();

    protected abstract void OnJump();
}