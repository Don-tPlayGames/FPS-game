using FPSgame.Scripts.Base.Input;
using FPSgame.Scripts.Node.Input;
using Godot;

namespace FPSgame.Scripts.Node.Character;

public partial class CharacterState : StateNode<CharacterStateMachine>
{
    [Export] protected NodePath CollisionShapePath;

    [Export] protected float CharacterEyeHeight;
    [Export] protected Curve EyeHeightInterpolationCurve = new ();

    public virtual float EyeHeight => CharacterEyeHeight;
    public virtual bool CanMove => true;
    public virtual bool CanJump => true;

    protected CollisionShape3D CollisionShape;

    protected IMovementInputMap Input;

    protected PlayerCharacterStats CharacterStats;

    public override void Init(CharacterStateMachine parent)
    {
        base.Init(parent);
        CollisionShape = GetNode<CollisionShape3D>(CollisionShapePath);
        Input = PlayerInputManager.Default;
        CharacterStats = GetNode<PlayerCharacterStats>("Stats");
    }

    public override void Enter()
    {
        CollisionShape.Disabled = false;
    }

    public override void Exit()
    {
        CollisionShape.Disabled = true;
    }
}