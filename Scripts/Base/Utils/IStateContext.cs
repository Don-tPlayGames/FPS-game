namespace FPSgame.Scripts.Base.Utils;

public interface IStateContext<T> where T : IStateState
{
    T DefaultState { get; }
    
    void SwitchState(T state);
}