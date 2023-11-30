using FPSgame.Scripts.Base.Character;

namespace FPSgame.Scripts.Node.Character;

public partial class CharacterStateBasicMovement : CharacterState
{
    public virtual float GetSpeedMultiplier() => 1.0f;
}