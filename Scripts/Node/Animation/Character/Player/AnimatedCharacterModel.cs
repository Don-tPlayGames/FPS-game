using System;
using FPSgame.Scripts.Node.Extension;
using Godot;

namespace FPSgame.Scripts.Node.Animation.Character.Player;

public partial class AnimatedCharacterModel : Node3D
{
    [Export] protected float RotationSpeed = 10.0f;

    [Export] protected AnimationTree Animator;

    [Export] protected BoneAttachment3D NeckAttachment;
    
    private float _targetRotation;

    public override void _Ready()
    {
        
    }

    public override void _Process(double deltaTime)
    {
        UpdateRotation(deltaTime);
        
        MoveSkin();
    }

    private void UpdateRotation(double deltaTime)
    {
        float rotationY = Mathf.LerpAngle(Rotation.Y, _targetRotation, (float)deltaTime * RotationSpeed);
        Rotation = new (0, rotationY, 0);
    }

    public Vector3 GetForwardVector()
    {
        return -Transform.Basis.Z;
    }

    public Vector3 GetRotation()
    {
        return Rotation;
    }

    public void SetFacing(Vector3 direction)
    {
        _targetRotation = Vector3.Forward.SignedAngleTo(direction.ToVector3Horizontal(), Vector3.Up);
    }

    public void SetMotionRelatively(Vector3 value)
    {
        float angle = GetForwardVector().ToVector3Horizontal().SignedAngleTo(Vector3.Forward, Vector3.Up);
        value = value.Rotated(Vector3.Up, angle);
        value = new(
            value.X,
            0.0f,
            -value.Z
        );
        
        SetMotion(value);
    }

    public void SetMotion(Vector3 value)
    {
        Animator.Set("parameters/Common/Movement/blend_position", value.ToVector2Horizontal());
        Animator.Set("parameters/Jump/Blend/blend_position", value.Z);
    }

    public void SetGrounded(bool value)
    {
        Animator.Set("parameters/conditions/IsGrounded", value);
    }
    
    public void SetJumping(bool value)
    {
        Animator.Set("parameters/conditions/IsJumping", value);
    }
    
    public void SetFalling(bool value)
    {
        Animator.Set("parameters/conditions/IsFalling", value);
    }

    private void MoveSkin()
    {
        Vector3 neckPos = NeckAttachment.GlobalPosition;
        Vector3 basePos = GlobalPosition - neckPos + Vector3.Up * 1.45f;
        Position = Position.Lerp(basePos, 0.8f);
    }
}