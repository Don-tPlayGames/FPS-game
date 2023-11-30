using System;

namespace FPSgame.Scripts.Base.Input;

public interface IMovementInputMap
{
    float Vertical { get; }
    float Horizontal { get; }
    
    float Sprint { get; }

    event Action Crouch;
    event Action Crawl;
    
    event Action Jump;
}