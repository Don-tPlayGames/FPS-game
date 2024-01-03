using Godot;

namespace FPSgame.Scripts.Node.Character.State;

public abstract partial class CharacterStatePoseAbstract : AbstractCharacterState
{
    [Export] public CollisionShape3D PoseCollisionShape { get; private set; }

    protected abstract float GetEyeHeight();

    public override void Enter()
    {
        base.Enter();
        PoseCollisionShape.Disabled = false;
        Context.Character.SetEyeHeight(GetEyeHeight());
    }

    public override void Exit()
    {
        base.Exit();
        PoseCollisionShape.Disabled = true;
    }
}