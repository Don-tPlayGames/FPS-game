namespace FPSgame.Scripts.Node.Character;

public partial class CharacterStateMachine : NodeStateMachine<CharacterState>
{
    protected override void InitState(CharacterState state)
    {
        state.Init(this);
        base.InitState(state);
    }
}