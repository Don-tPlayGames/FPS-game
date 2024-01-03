using FPSgame.Scripts.Node.Extension;
using Godot;

namespace FPSgame.Scripts.Node.Character.State;

public partial class CharacterStateMovementRun : CharacterStateMovementAbstract
{
    public override void PhysicsUpdate(double deltaTime)
    {
        base.PhysicsUpdate(deltaTime);

        if (Context.CharacterController.IsSprinting)
        {
            CurrentSuperState?.SwitchSubState(typeof(CharacterStateMovementSprint));
            return;
        }
        
        if (Context.Character.Velocity.ToVector2Horizontal().IsEqualApprox(Vector2.Zero))
            CurrentSuperState?.SwitchSubState(typeof(CharacterStateMovementIdle));
    }
}