using Godot;

namespace FPSgame.Scripts.Node.Character.Camera;

public class CameraNoise
{
    private readonly Vector3 _gain;

    private FastNoiseLite _noise;
    private int _seed;

    public CameraNoise(Vector3 gain)
    {
        _gain = gain;

        _noise = new FastNoiseLite();
        _noise.NoiseType = FastNoiseLite.NoiseTypeEnum.Simplex;
        
        RandomNumberGenerator rng = new();
        rng.Randomize();
        _seed = rng.RandiRange(0, int.MaxValue);
    }
    
    public Vector3 Get(float power, float time)
    {
        if (Mathf.IsZeroApprox(power))
            return Vector3.Zero;

        Vector3 result = _gain;
        for (int i = 0; i < 3; i++)
        {
            _noise.Seed = _seed + i;
            result[i] *= _noise.GetNoise1D(time) * power;
        }

        return result;
    }

    public Vector3 GetSine(float power, Vector3 timeScale)
    {
        if (Mathf.IsZeroApprox(power))
            return Vector3.Zero;

        Vector3 result = _gain * power;
        for (int i = 0; i < 3; i++)
            result[i] *= Mathf.Sin(Time.GetTicksMsec() * timeScale[i]);
        
        return result;
    }
}