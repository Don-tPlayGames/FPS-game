using Godot;

namespace FPSgame.Scripts.Node.Character.Camera;

public interface ICamera
{
    Vector3 LookVector { get; }
    
    void SetEyeHeight(float value);

    void SetFieldOfView(float degrees);
    
    void ResetFieldOfView();
}