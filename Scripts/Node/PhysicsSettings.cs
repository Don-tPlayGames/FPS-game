using FPSgame.Scripts.Base.World;
using Godot;

namespace FPSgame.Scripts.Node;

public class PhysicsSettings : IPhysicsSettings
{
    private float? _gravity;
    public float GlobalGravity
    {
        get
        {
            if(_gravity is null)
                _gravity = ProjectSettings.GetSetting("physics/3d/default_gravity").AsSingle();
            return _gravity.Value;
        }
    }
}