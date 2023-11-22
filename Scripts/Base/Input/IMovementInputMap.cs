using System;

namespace FPSgame.Scripts.Base.Input;

public interface IMovementInputMap
{
    float Vertical { get; }
    float Horizontal { get; }
    
    float Sprint { get; }
    float Crouch { get; }
    
    event Action Jump;
}