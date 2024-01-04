using FPSgame.Scripts.Base.Character.Attributes;
using Godot;

namespace FPSgame.Scripts.Node.Character;

public partial class PlayerCharacterStats : Godot.Node, ICharacterBodyStats, ICharacterPhysicalStats
{
    [Export] private float _mass = 70.0f;
    public AttributeFloat Mass { get; private set; }
    
    [Export] public float Height { get; internal set; } = 1.8f;
    [Export] public float HeightCrouching { get; internal set; } = 1.0f;
    [Export] public float HeightCrawling { get; internal set; } = 0.6f;
    
    [Export] public float EyeHeightNormal { get; internal set; } = 1.65f;
    [Export] public float EyeHeightCrouching { get; internal set; } = 0.75f;
    [Export] public float EyeHeightCrawling { get; internal set; } = 0.3f;


    [Export] private float _baseSpeed = 5.0f;
    public AttributeFloat MovementSpeed { get; private set; }
    
    [Export] public float SprintSpeedMul { get; internal set; } = 1.4f;
    [Export] public float CrouchSpeedMul { get; internal set; } = 0.5f;
    [Export] public float CrawlSpeedMul { get; internal set; } = 0.3f;


    [Export] private float JumpHeight { get; set; } = 0.7f;
    public float JumpVelocity => Mathf.Sqrt(2 * JumpHeight * WorldSettings.Physics.GlobalGravity);
    
    [Export] public float JumpCooldown { get; internal set; } = 0.3f;

    public override void _Ready()
    {
        Mass = new (_mass);
        
        MovementSpeed = new(_baseSpeed);
    }
}