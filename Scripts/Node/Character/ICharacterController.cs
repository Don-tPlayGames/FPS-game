using System;
using Godot;

namespace FPSgame.Scripts.Node.Character;

public interface ICharacterController
{
    Vector3 HorizontalMovement { get; }

    bool IsSprinting { get; }

    event Action OnSwitchCrouch;
    event Action OnSwitchCrawl;
    
    event Action OnJump;
}