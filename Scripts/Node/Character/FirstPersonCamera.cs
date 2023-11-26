using FPSgame.Scripts.Base.Input;
using FPSgame.Scripts.Node.Input;
using Godot;

namespace FPSgame.Scripts.Node.Character;

public partial class FirstPersonCamera : Node3D
{
    [Export] private float SensitivityX = 0.2f;
    [Export] private float SensitivityY = 0.2f;

    [Export] private float PitchMin = -85.0f;
    [Export] private float PitchMax = 85.0f;

    public Vector3 LookVector => -_camera.GlobalTransform.Basis.Z;

    private Node3D _pitchJoint;
    private Camera3D _camera;

    private ICameraInputMap _input;

    public override void _Ready()
    {
        _pitchJoint = GetChild<Node3D>(0);
        _camera = _pitchJoint.GetChild<Camera3D>(0, true);
        
        _input = PlayerInputManager.Default;

        Godot.Input.MouseMode = Godot.Input.MouseModeEnum.Captured;
    }

    public override void _PhysicsProcess(double delta)
    {
        RotateY(-_input.MouseX * SensitivityX * (float)delta);
        
        float targetPitch = _pitchJoint.Rotation.X - _input.MouseY * SensitivityY * (float)delta;
        targetPitch = Mathf.Clamp(targetPitch, Mathf.DegToRad(PitchMin), Mathf.DegToRad(PitchMax));
        _pitchJoint.Rotation = new Vector3(targetPitch, 0, 0);
    }
}