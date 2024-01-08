namespace FPSgame.Scripts.Base.Character.Attributes;

public interface ICharacterBodyStats
{
    AttributeFloat Mass { get; }
    
    float Height { get; }
    
    float HeightCrouching { get; }
    
    float HeightCrawling { get; }
    
    float EyeHeightNormal { get; }
    
    float EyeHeightCrouching { get; }
    
    float EyeHeightCrawling { get; }
}