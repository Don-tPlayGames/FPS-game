using FPSgame.Scripts.Node.Extension;
using Godot;

namespace FPSgame.Scripts.Node.Character.State;

public abstract partial class CharacterStateMovementAbstract : AbstractCharacterState
{
    public override void PhysicsUpdate(double deltaTime)
    {
        base.PhysicsUpdate(deltaTime);
        
        Move();
    }

    protected virtual Vector3 CalculateVelocity()
    {
        return Context.CharacterController.HorizontalMovement * Context.Character.Stats.BaseSpeed;
    }

    protected virtual void Move()
    {
        Context.Character.SetTargetVelocity(CalculateVelocity().ToVector2Horizontal());
    }
}