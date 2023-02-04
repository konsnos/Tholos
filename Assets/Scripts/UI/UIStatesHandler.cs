using UnityEngine;

public class UIStatesHandler : MonoBehaviour
{
    [SerializeField] private JoinUI joinUI;
    [SerializeField] private PlayUI playUI;

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
            case GameStateType.Play:
                playUI.gameObject.SetActive(false);
                break;
        }

        switch (newGameState)
        {
            case GameStateType.Join:
                joinUI.gameObject.SetActive(true);
                break;
            case GameStateType.Play:
                playUI.gameObject.SetActive(true);
                break;
        }
    }
}
