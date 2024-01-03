namespace FPSgame.Scripts.Node.Character.State;

public partial class CharacterStatePoseStanding : CharacterStatePoseBasic
{
    protected override float GetEyeHeight()
    {
        return Context.Character.Stats.EyeHeightNormal;
    }
    
    protected override void OnSwitchCrouch()
    {
        CurrentSuperState?.SwitchSubState(typeof(CharacterStatePoseCrouching));
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