using UnityEngine;

public class GameManager : MonoBehaviour
{
    private GameStateType _gameStateType = GameStateType.None;
    private IGameState _gameState;
    
    [SerializeField] private JoystickVRPN _seatsVRPN;
    [SerializeField] private JoystickVRPN _navigatorVRPN;

    private void Start()
    {
        _gameStateType = GameStateType.Join;
        
        _seatsVRPN.OnButtonChange.AddListener(OnButtonClick);
        _navigatorVRPN.OnAxisChange.AddListener(OnAxisChanged);
        
        UpdateGameState();
    }

    private void Update()
    {
        UpdateGame();
    }

    private void UpdateGame()
    {
        _gameState?.UpdateManual();
    }

    private void UpdateGameState()
    {
        Debug.Log($"Stopping game state: {_gameState}", gameObject);
        _gameState?.OnStateClose.RemoveListener(OnStateClosed);
        _gameState?.Stop();

        _gameState = _gameStateType switch
        {
            GameStateType.Join => FindObjectOfType<JoinState>(),
            GameStateType.Play => FindObjectOfType<PlayState>(),
            _ => null
        };

        _gameState?.OnStateClose.AddListener(OnStateClosed);
        _gameState?.Begin();
        
        Debug.Log($"New game state: {_gameState}", gameObject);
    }

    private void OnStateClosed()
    {
        AssignNextState();
    }

    private void AssignNextState()
    {
        _gameStateType = _gameStateType switch
        {
            GameStateType.Join => GameStateType.Play
        };
    }

    private void OnAxisChanged(int number, int channel, double value)
    {
        if (_gameState == null) return;
        
        _gameState.OnAxisChange(number, channel, value);
    }

    private void OnButtonClick(int number, int button, bool state)
    {
        if (_gameState == null) return;
        
        _gameState.OnButtonClick(number, button, state);
    }
}

public enum GameStateType
{
    None,
    Join,
    Play
}