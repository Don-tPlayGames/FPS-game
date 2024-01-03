using System;

namespace FPSgame.Scripts.Base.Input;

public interface IMovementInputMap
{
    float Vertical { get; }
    float Horizontal { get; }
    
    bool Sprint { get; }

    event Action OnCrouchKeyPressed;
    event Action OnCrawlKeyPressed;
    
    event Action OnJumpKeyPressed;
}