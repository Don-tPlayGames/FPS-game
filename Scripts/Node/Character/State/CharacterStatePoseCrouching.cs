namespace FPSgame.Scripts.Node.Character.State;

public partial class CharacterStatePoseCrouching : CharacterStatePoseBasic
{
    protected override float GetEyeHeight()
    {
        return Context.Character.Stats.EyeHeightCrouch;
    }

    protected override void OnSwitchCrouch()
    {
        CurrentSuperState?.SwitchSubState(typeof(CharacterStatePoseStanding));
    }

    protected override void OnSwitchCrawl()
    {
        CurrentSuperState?.SwitchSubState(typeof(CharacterStatePoseCrawling));
    }

    protected override void OnJump()
    {
        Context.Character.TryPerformJump();
    }
}