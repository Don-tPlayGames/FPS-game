namespace FPSgame.Scripts.Base.Character;

public interface ICharacterBodyStats
{
    float Mass { get; }
    
    float EyeHeightNormal { get; }
    
    float EyeHeightCrouch { get; }
    
    float EyeHeightCrawl { get; }
}