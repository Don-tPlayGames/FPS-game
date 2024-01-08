namespace FPSgame.Scripts.Base.Character.Attributes;

public interface ICharacterPhysicalStats
{
    AttributeFloat MovementSpeed { get; }
    
    float SprintSpeedMul { get; }
    
    float CrouchSpeedMul { get; }
    float CrawlSpeedMul { get; }
    
    float JumpVelocity { get; }
    float JumpCooldown { get; }
}