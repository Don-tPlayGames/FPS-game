using FPSgame.Scripts.Node.Extension;
using Godot;

namespace FPSgame.Scripts.Node.Character.State;

public partial class CharacterStateMovementSprint : CharacterStateMovementAbstract
{
    protected override Vector3 CalculateVelocity()
    {
        return base.CalculateVelocity() * Context.Character.Stats.SprintSpeedMul;
    }

    public override void PhysicsUpdate(double deltaTime)
    {
        base.PhysicsUpdate(deltaTime);

        if (!Context.CharacterController.IsSprinting)
        {
            CurrentSuperState?.SwitchSubState(typeof(CharacterStateMovementRun));
            return;
        }

        if (Context.Character.Velocity.ToVector2Horizontal().IsEqualApprox(Vector2.Zero))
        {
            CurrentSuperState?.SwitchSubState(typeof(CharacterStateMovementIdle));
        }
    }

    private void OnSprintStop()
    {
        CurrentSuperState?.SwitchSubState(typeof(CharacterStateMovementRun));
    }
}