using System;
using UnityEngine;

public class PlayState : BaseGameState
{
    private CharactersManager _charactersManager;

    public delegate void OnPlayerButtonDelegate(int playerId, int button);
    public event OnPlayerButtonDelegate OnPlayerButton;

    private DateTime _startTime;
    private DateTime _endTime;
    [SerializeField] private int _sessionDuration;
    public int SecondsRemaining { private set; get; }

    private void Awake()
    {
        _charactersManager = FindObjectOfType<CharactersManager>();
    }

    public override void Begin()
    {
        _charactersManager.SubscribeToCharacterMovement(this);
        _charactersManager.AddListenerForWaterTouch();

        _startTime = DateTime.UtcNow;
        _endTime = _startTime + new TimeSpan(0, 0, _sessionDuration);
        SecondsRemaining = _sessionDuration;
    }

    public override void Stop()
    {
        _charactersManager.UnsubscribeFromCharacterMovement();
    }

    public override void UpdateManual()
    {
        if(_charactersManager.Characters.Count <= 0)
        {
            CloseState();
            return;
        }

        if(DateTime.UtcNow < _endTime)
        {
            SecondsRemaining = Mathf.CeilToInt((float)(_endTime - DateTime.UtcNow).TotalSeconds);
        }
        else
        {
            SecondsRemaining = 0;
            CloseState();
        }
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