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

    [Export] private float _neckAngleMax = 60.0f;
    
    public ICamera Camera { get; private set; }
    
    public Vector3 HorizontalMovement { get; private set; }
    
    public bool IsSprinting { get; private set; }

    public event Action OnSwitchCrouch;
    public event Action OnSwitchCrawl;
    
    public event Action OnJump = () => { };

    private IMovementInputMap _movementInput;

    private CharacterBasic _character;
    
    public override void _Ready()
    {
        Camera = GetNode<ICamera>(_camera);
        
        _movementInput = PlayerInputManager.Default;

        _movementInput.OnCrouchKeyPressed += () => OnSwitchCrouch?.Invoke();
        _movementInput.OnCrawlKeyPressed += () => OnSwitchCrawl?.Invoke();

        _movementInput.OnJumpKeyPressed += () => OnJump?.Invoke();

        _character = GetParent<CharacterBasic>();
    }

    public override void _PhysicsProcess(double delta)
    {
        Vector3 movementInput = new Vector3(_movementInput.Horizontal, 0, _movementInput.Vertical).Normalized();
        float cameraGlobalRotationY = Vector3.Forward.SignedAngleTo(Camera.LookVector.ToVector3Horizontal(), Vector3.Up);
        
        HorizontalMovement = movementInput.Rotated(Vector3.Up, cameraGlobalRotationY);
        
        CheckCanSprint();
    }

    public override void _Process(double delta)
    {
        RotateCharacterModel();
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
        {
            Camera.SetFieldOfView(IsSprinting ? _cameraFovSprinting : _cameraFovNormal);
            //TODO: remove/replace this
            Camera.SetHeadBob(IsSprinting ? 10.0f : 0.0f);
        }
    }

    private void RotateCharacterModel()
    {
        if (HorizontalMovement.IsZeroApprox())
        {
            float cameraRotation = Vector3.Forward.SignedAngleTo(Camera.LookVector.ToVector3Horizontal(), Vector3.Up);
            float angle = Mathf.Abs(cameraRotation - _character.Model.GlobalRotation.Y);

            if (angle > Mathf.DegToRad(_neckAngleMax))
            {
                _character.Model.SetFacing(Camera.LookVector);
            }
        }
        else
        {
            if (IsSprinting)
            {
                _character.Model.SetFacing(HorizontalMovement);
            }
            else
            {
                _character.Model.SetFacing(Camera.LookVector);
            }
        }
    }
}