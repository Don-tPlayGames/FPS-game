namespace FPSgame.Scripts.Base.Character;

public interface ICharacterPhysicalStats
{
    float BaseSpeed { get; }
    float SprintSpeedMul { get; }
    float CrouchSpeedMul { get; }
    float CrawlSpeedMul { get; }
    
    float JumpVelocity { get; }
    float JumpCooldown { get; }
}