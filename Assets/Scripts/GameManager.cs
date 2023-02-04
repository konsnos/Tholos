using System;
using UnityEngine;
using UnityEngine.Events;

public class GameManager : MonoBehaviour
{
    public static GameManager Singleton { get; private set; }

    private GameStateType gameStateType = GameStateType.None;

    [SerializeField] private JoystickVRPN _seatsVRPN;
    [SerializeField] private JoystickVRPN _navigatorVRPN;

    public IGameState GameState { get; private set; }
    public StateChangedEvent OnStateChanged;

    private void Awake()
    {
        Singleton = this;
    }

    private void Start()
    {
        gameStateType = GameStateType.Join;

        _seatsVRPN.OnButtonChange.AddListener(OnButtonClick);
        _navigatorVRPN.OnAxisChange.AddListener(OnAxisChanged);

        UpdateGameState(GameStateType.Join);
    }

    private void Update()
    {
        UpdateGame();
    }

    private void UpdateGame()
    {
        GameState?.UpdateManual();
    }

    public void GoToPlay()
    {
        UpdateGameState(GameStateType.Play);
    }

    private void UpdateGameState(GameStateType newGameStateType)
    {
        Debug.Log($"Stopping game state: {gameStateType}", gameObject);
        var oldGameState = gameStateType;
        GameState?.OnStateClose.RemoveListener(OnStateClosed);
        GameState?.Stop();
        gameStateType = newGameStateType;

        GameState = gameStateType switch
        {
            GameStateType.Join => FindObjectOfType<JoinState>(),
            GameStateType.Play => FindObjectOfType<PlayState>(),
            GameStateType.End => FindObjectOfType<EndState>(),
            _ => null
        };

        GameState?.OnStateClose.AddListener(OnStateClosed);
        GameState?.Begin();

        OnStateChanged?.Invoke(gameStateType);

        Debug.Log($"New game state: {GameState}", gameObject);
    }

    private void OnStateClosed()
    {
        AssignNextState();
    }

    private void AssignNextState()
    {
        Debug.Log($"Assigning next state from {gameStateType}");
        gameStateType = gameStateType switch
        {
            GameStateType.Join => GameStateType.Play,
            GameStateType.Play => GameStateType.End,
        };
        UpdateGameState(gameStateType);
    }

    private void OnAxisChanged(int number, int channel, double value)
    {
        if (GameState == null) return;

        GameState.OnAxisChange(number, channel, value);
    }

    private void OnButtonClick(int number, int button, bool state)
    {
        if (GameState == null) return;

        GameState.OnButtonClick(number, button, state);
    }
}

public enum GameStateType
{
    None,
    Join,
    Play,
    End,
}

[Serializable]
public class StateChangedEvent : UnityEvent<GameStateType>
{ }