using System;

public class PlayState : BaseGameState
{
    private CharactersManager _charactersManager;

    public delegate void OnPlayerButtonDelegate(int playerId, int button);
    public event OnPlayerButtonDelegate OnPlayerButton;

    private void Awake()
    {
        _charactersManager = FindObjectOfType<CharactersManager>();
    }

    public override void Begin()
    {
        _charactersManager.SubscribeToCharacterMovement(this);
    }

    public override void Stop()
    {
        _charactersManager.UnsubscribeFromCharacterMovement();
    }

    public override void UpdateManual()
    {
        // do nothing
    }

    public override void OnButtonClick(int joystick, int button, bool state)
    {
        if (!state) return;

        OnPlayerButton?.Invoke(joystick, button);
    }

    public override void OnAxisChange(int joystick, int channel, double value)
    {
        throw new NotImplementedException();
    }
}