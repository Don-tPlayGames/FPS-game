using FPSgame.Scripts.Base.Character.Attributes;
using FPSgame.Scripts.Node.Extension;
using Godot;

namespace FPSgame.Scripts.Node.Character.State;

public partial class CharacterStateMovementSprint : CharacterStateMovementAbstract
{
    private AttributeModifier _speedModifier;
    
    public override void Enter()
    {
        base.Enter();

        if (_speedModifier is null)
            _speedModifier = new AttributeModifier(
                Context.Character.Stats.SprintSpeedMul,
                AttributeModifier.ModifierOperation.Multiply);

        Context.Character.Stats.MovementSpeed.AddModifier(_speedModifier);
    }

    public override void Exit()
    {
        base.Exit();
        
        if (_speedModifier is not null)
            Context.Character.Stats.MovementSpeed.RemoveModifier(_speedModifier);
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