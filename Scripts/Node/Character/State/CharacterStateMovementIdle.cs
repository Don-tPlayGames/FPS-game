using FPSgame.Scripts.Node.Extension;
using Godot;

namespace FPSgame.Scripts.Node.Character.State;

public partial class CharacterStateMovementIdle : CharacterStateMovementAbstract
{
    public override void PhysicsUpdate(double deltaTime)
    {
        base.PhysicsUpdate(deltaTime);
        
        if (!Context.Character.Velocity.ToVector2Horizontal().IsEqualApprox(Vector2.Zero))
            CurrentSuperState?.SwitchSubState(typeof(CharacterStateMovementRun));
    }
}