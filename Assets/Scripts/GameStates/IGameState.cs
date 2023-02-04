using UnityEngine.Events;

public interface IGameState
{
    UnityEvent OnStateClose { get; }
    
    void Begin();
    void Stop();

    void UpdateManual();
    
    void OnButtonClick(int joystick, int button, bool state);
    void OnAxisChange(int joystick, int channel, double value);
}