using FPSgame.Scripts.Base.Input;
using FPSgame.Scripts.Node.Character.Camera;
using FPSgame.Scripts.Node.Input;
using Godot;

namespace FPSgame.Scripts.Node.Character;

public partial class FirstPersonCamera : Node3D, ICamera
{
    [Export] private float _sensitivityX = 0.2f;
    [Export] private float _sensitivityY = 0.2f;

    [Export] private float _pitchMin = -85.0f;
    [Export] private float _pitchMax = 85.0f;

    [Export] private Vector3 _pitchJointOffset = new(0.0f, 0.2f, 0.15f);

    [Export] private float _interpolationWeight = 0.6f;

    public Vector3 LookVector => -_camera.GlobalTransform.Basis.Z;

    private Node3D _pitchJoint;
    private Camera3D _camera;

    private float _targetEyeHeight;

    private ICameraInputMap _input;

    private float _deafaultFov;

    public override void _Ready()
    {
        _pitchJoint = GetChild<Node3D>(0);
        _camera = _pitchJoint.GetChild<Camera3D>(0, true);

        _deafaultFov = _camera.Fov;
        
        _input = PlayerInputManager.Default;
    }

    public override void _PhysicsProcess(double delta)
    {
        RotateY(-_input.MouseX * _sensitivityX * (float)delta);
        
        float targetPitch = _pitchJoint.Rotation.X - _input.MouseY * _sensitivityY * (float)delta;
        targetPitch = Mathf.Clamp(targetPitch, Mathf.DegToRad(_pitchMin), Mathf.DegToRad(_pitchMax));
        _pitchJoint.Rotation = new Vector3(targetPitch, 0, 0);
    }

    public override void _Process(double deltaTime)
    {
        Position = Position.Lerp(new Vector3(Position.X, _targetEyeHeight, Position.Z), _interpolationWeight);
    }

    public void SetEyeHeight(float value)
    {
        _targetEyeHeight = value - _pitchJointOffset.Y;
    }

    public void SetFieldOfView(float degrees)
    {
        _camera.Fov = degrees;
    }

    public void ResetFieldOfView()
    {
        _camera.Fov = _deafaultFov;
    }
}