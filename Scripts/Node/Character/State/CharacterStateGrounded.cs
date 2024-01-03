using System;

namespace FPSgame.Scripts.Node.Character.State;

public partial class CharacterStateGrounded : AbstractCharacterState
{
    private Type _lastSubState = typeof(CharacterStatePoseStanding);
    
    public override void Enter()
    {
        Context.Character.IsGravityApplied = true;
        
        Context.Character.SetJumpCooldown(Context.Character.Stats.JumpCooldown);

        SwitchSubState(_lastSubState);
    }

    public override void Exit()
    {
        _lastSubState = CurrentSubState?.GetType() ?? typeof(CharacterStatePoseStanding);
        SwitchSubState(typeof(CharacterStatePoseStanding));
        
        CurrentSubState?.Exit();
    }

    public override void PhysicsUpdate(double deltaTime)
    {
        base.PhysicsUpdate(deltaTime);
        
        if (!Context.Character.IsOnFloor())
        {
            Context.SwitchState(typeof(CharacterStateFalling));
        }
    }
}