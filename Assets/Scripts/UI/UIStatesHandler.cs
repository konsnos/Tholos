using UnityEngine;

public class UIStatesHandler : MonoBehaviour
{
    [SerializeField] private JoinUI joinUI;

    private void Start()
    {
        GameManager.Singleton.OnStateChanged.AddListener(OnGameStateChanged);
    }

    private void OnGameStateChanged(GameStateType oldGameState, GameStateType newGameState)
    {
        switch (oldGameState)
        {
            case GameStateType.Join:
                joinUI.gameObject.SetActive(false); 
                break;
        }

        switch (newGameState)
        {
            case GameStateType.Join:
                joinUI.gameObject.SetActive(true);
                break;
        }
    }
}
