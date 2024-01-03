namespace FPSgame.Scripts.Node.Character.State;

public partial class CharacterStatePoseCrawling : CharacterStatePoseBasic
{
    protected override float GetEyeHeight()
    {
        return Context.Character.Stats.EyeHeightCrawl;
    }

    protected override void OnSwitchCrouch()
    {
        CurrentSuperState?.SwitchSubState(typeof(CharacterStatePoseCrouching));
    }

    protected override void OnSwitchCrawl()
    {
        CurrentSuperState?.SwitchSubState(typeof(CharacterStatePoseStanding));
    }

    protected override void OnJump()
    {
        CurrentSuperState?.SwitchSubState(typeof(CharacterStatePoseStanding));
    }
}