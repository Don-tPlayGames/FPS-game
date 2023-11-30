namespace FPSgame.Scripts.Node.Character;

public partial class CharacterStateCrawling : CharacterStateBasicMovement
{
    public override bool CanJump => false;

    public override float GetSpeedMultiplier() => CharacterStats.CrawlSpeedMul;
}