using FPSgame.Scripts.Base.Character.Attributes;

namespace FPSgame.Scripts.Node.Character.State;

public partial class CharacterStatePoseCrawling : CharacterStatePoseBasic
{
    private AttributeModifier _speedModifier;
    
    public override void Enter()
    {
        base.Enter();

        if (_speedModifier is null)
            _speedModifier = new AttributeModifier(
                Context.Character.Stats.CrawlSpeedMul,
                AttributeModifier.ModifierOperation.Multiply);

        Context.Character.Stats.MovementSpeed.AddModifier(_speedModifier);
    }

    public override void Exit()
    {
        base.Exit();
        
        if (_speedModifier is not null)
            Context.Character.Stats.MovementSpeed.RemoveModifier(_speedModifier);
    }
    
    protected override float GetEyeHeight()
    {
        return Context.Character.Stats.EyeHeightCrawling;
    }

    protected override void OnSwitchCrouch()
    {
        if (!Context.Character.CanStandUp(Context.Character.Stats.HeightCrouching))
            return;
        
        CurrentSuperState?.SwitchSubState(typeof(CharacterStatePoseCrouching));
    }

    protected override void OnSwitchCrawl()
    {
        TryStandUp();
    }

    protected override void OnJump()
    {
        TryStandUp();
    }

    protected virtual void TryStandUp()
    {
        if (!Context.Character.CanStandUp(Context.Character.Stats.Height))
        {
            if (Context.Character.CanStandUp(Context.Character.Stats.HeightCrouching))
                CurrentSuperState?.SwitchSubState(typeof(CharacterStatePoseCrouching));
            
            return;
        }
        
        CurrentSuperState?.SwitchSubState(typeof(CharacterStatePoseStanding));
    }
}