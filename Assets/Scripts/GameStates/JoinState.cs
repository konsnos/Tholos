using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class JoinState : BaseGameState
{
    private readonly HashSet<int> _playersExisting = new HashSet<int>();
    private CharactersManager _charactersManager;

    public delegate void OnPlayerAddedDelegate(int playerId);
    public event OnPlayerAddedDelegate OnPlayerAdded;

    private DateTime _startTime;
    [SerializeField] private int _secondsForPlayerCreation;
    private DateTime _endTime;
    public SecondsRemainingEvent _onSecondsRemaingEvent;
    private int _lastSeconds;

    private void Awake()
    {
        _charactersManager = FindObjectOfType<CharactersManager>();
    }

    public override void UpdateManual()
    {
        return; // manual closing from display 1
        if (DateTime.UtcNow <= _endTime)
        {
            int remainingSeconds = (_endTime - DateTime.UtcNow).Seconds;

            if (_lastSeconds > remainingSeconds)
            {
                _lastSeconds = remainingSeconds;
                _onSecondsRemaingEvent?.Invoke(remainingSeconds);
                
                Debug.Log($"Seconds remaining: {remainingSeconds}", gameObject);
            }
        }
        else
        {
            CloseState();
        }
    }

    public override void Begin()
    {
        _playersExisting.Clear();
        _charactersManager.SubscribeToPlayerCreation(this);

        StartCounter();
    }

    private void StartCounter()
    {
        _startTime = DateTime.UtcNow;
        _endTime = _startTime + new TimeSpan(0, 0, 0, _secondsForPlayerCreation);
        _lastSeconds = _secondsForPlayerCreation;
        _onSecondsRemaingEvent?.Invoke(_lastSeconds);
    }

    public override void Stop()
    {
        _charactersManager.UnsubscribeFromPlayerCreation();
    }

    public override void OnButtonClick(int number, int button, bool state)
    {
        if (_playersExisting.Contains(number)) return;

        OnPlayerAdded?.Invoke(number);

        _playersExisting.Add(number);
    }

    public override void OnAxisChange(int joystick, int channel, double value)
    {
        // do nothing
    }
}

[System.Serializable]
public class SecondsRemainingEvent : UnityEvent<int>
{
}