using UnityEngine;
using UnityEngine.Events;

public abstract class BaseGameState : MonoBehaviour, IGameState
{
    protected UnityEvent _onStateClosed = new UnityEvent();
    public UnityEvent OnStateClose => _onStateClosed;

    public abstract void Begin();

    public abstract void Stop();
    
    public abstract void UpdateManual();

    public abstract void OnButtonClick(int joystick, int button, bool state);

    public abstract void OnAxisChange(int joystick, int channel, double value);

    protected virtual void CloseState()
    {
        OnStateClose?.Invoke();
    }
}