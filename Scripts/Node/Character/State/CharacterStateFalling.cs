namespace FPSgame.Scripts.Node.Character.State;

public partial class CharacterStateFalling : AbstractCharacterState
{
    public override void Enter()
    {
        base.Enter();
        
        Context.Character.IsGravityApplied = true;
        
        SwitchSubState(typeof(CharacterStatePoseIndefinite));
    }

    public override void PhysicsUpdate(double deltaTime)
    {
        if (Context.Character.IsOnFloor())
        {
            Context.SwitchState(typeof(CharacterStateGrounded));
        }
    }
}