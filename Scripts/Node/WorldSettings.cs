using FPSgame.Scripts.Base.World;

namespace FPSgame.Scripts.Node;

public static class WorldSettings
{
    public static IPhysicsSettings Physics { get; } = new PhysicsSettings();
}