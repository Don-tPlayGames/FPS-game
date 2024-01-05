namespace FPSgame.Scripts.Node.Character.State;

public partial class CharacterStateGrounded : AbstractCharacterState
{
    public override void Enter()
    {
        Context.Character.IsGravityApplied = true;
        
        Context.Character.SetJumpCooldown(Context.Character.Stats.JumpCooldown);

        SwitchSubState(typeof(CharacterStatePoseStanding));
    }

    public override void Exit()
    {
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