namespace FPSgame.Scripts.Node.Character;

public partial class CharacterStateCrouching : CharacterStateBasicMovement
{
    public override float GetSpeedMultiplier() => CharacterStats.CrouchSpeedMul;
}