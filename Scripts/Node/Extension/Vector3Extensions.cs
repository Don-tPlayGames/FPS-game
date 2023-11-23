using Godot;

namespace FPSgame.Scripts.Node.Extension;

public static class Vector3Extensions
{
    public static Vector2 ToVector2Horizontal(this Vector3 vector3) => new (vector3.X, vector3.Z);
}