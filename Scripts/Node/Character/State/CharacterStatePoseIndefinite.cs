namespace FPSgame.Scripts.Node.Character.State;

public partial class CharacterStatePoseIndefinite : CharacterStatePoseAbstract
{
    protected override float GetEyeHeight()
    {
        return Context.Character.Stats.EyeHeightNormal;
    }
}