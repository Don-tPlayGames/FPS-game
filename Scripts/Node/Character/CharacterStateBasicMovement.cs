namespace FPSgame.Scripts.Node.Character;

public partial class CharacterStateBasicMovement : CharacterState
{
    public virtual float GetSpeedMultiplier() => 1.0f;

    protected CharacterBasic Controller;

    public override void Init(CharacterStateMachine parent)
    {
        base.Init(parent);
        Controller = StateMachine.GetParent<CharacterBasic>();
    }
}