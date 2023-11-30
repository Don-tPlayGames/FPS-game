using FPSgame.Scripts.Base.Character;
using Godot;

namespace FPSgame.Scripts.Node.Character;

public partial class PlayerCharacterStats : Godot.Node, ICharacterBodyStats, ICharacterPhysicalStats
{
    [Export] public float Mass { get; internal set; } = 70.0f;


    [Export] public float BaseSpeed { get; internal set;  } = 5.0f;
    public float SprintSpeedMul { get; internal set; } = 1.4f;
    public float CrouchSpeedMul { get; internal set; } = 0.5f;
    public float CrawlSpeedMul { get; internal set; } = 0.3f;


    [Export] private float JumpHeight { get; set; } = 0.7f;
    public float JumpVelocity => Mathf.Sqrt(2 * JumpHeight * WorldSettings.Physics.GlobalGravity);
    
    [Export] public float JumpCooldown { get; internal set; } = 0.3f;
}