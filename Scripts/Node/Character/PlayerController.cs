using System;
using FPSgame.Scripts.Base.Input;
using FPSgame.Scripts.Node.Character.Camera;
using FPSgame.Scripts.Node.Extension;
using FPSgame.Scripts.Node.Input;
using Godot;

namespace FPSgame.Scripts.Node.Character;

public partial class PlayerController : Godot.Node, ICharacterController
{
    [Export] private NodePath _camera;

    [Export] private float _cameraFovNormal = 70.0f;
    [Export] private float _cameraFovSprinting = 80.0f;
    
    public ICamera Camera { get; private set; }
    
    public Vector3 HorizontalMovement { get; private set; }
    
    public bool IsSprinting { get; private set; }

    public event Action OnSwitchCrouch;
    public event Action OnSwitchCrawl;
    
    public event Action OnJump = () => { };

    private IMovementInputMap _movementInput;

    private bool _isSprinting = false;
    
    public override void _Ready()
    {
        Camera = GetNode<ICamera>(_camera);
        
        _movementInput = PlayerInputManager.Default;

        _movementInput.OnCrouchKeyPressed += () => OnSwitchCrouch?.Invoke();
        _movementInput.OnCrawlKeyPressed += () => OnSwitchCrawl?.Invoke();

        _movementInput.OnJumpKeyPressed += () => OnJump?.Invoke();
    }

    public override void _PhysicsProcess(double delta)
    {
        Vector3 movementInput = new Vector3(_movementInput.Horizontal, 0, _movementInput.Vertical).Normalized();
        float cameraGlobalRotationY = Vector3.Forward.SignedAngleTo(Camera.LookVector.ToVector3Horizontal(), Vector3.Up);
        
        HorizontalMovement = movementInput.Rotated(Vector3.Up, cameraGlobalRotationY);
        
        CheckCanSprint();
    }

    private void CheckCanSprint()
    {
        bool flag = IsSprinting;
        
        if (!_movementInput.Sprint)
        {
            IsSprinting = false;
        }
        else
        {
            Vector3 movementInput = new Vector3(_movementInput.Horizontal, 0, _movementInput.Vertical).Normalized();

            if (!movementInput.IsEqualApprox(Vector3.Zero))
            {
                float moveAngle = Mathf.RadToDeg(movementInput.AngleTo(Vector3.Forward));
                IsSprinting = moveAngle <= 60.0f;
            }
            else IsSprinting = false;
        }

        if (IsSprinting != flag)
            Camera.SetFieldOfView(IsSprinting ? _cameraFovSprinting : _cameraFovNormal);
    }
}