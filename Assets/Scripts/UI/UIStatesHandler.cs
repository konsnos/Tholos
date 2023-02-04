using UnityEngine;

public class UIStatesHandler : MonoBehaviour
{
    [SerializeField] private JoinUI joinUI;
    [SerializeField] private PlayUI playUI;
    [SerializeField] private EndUI endUI;

    private void Awake()
    {
        GameManager.Singleton.OnStateChanged.AddListener(OnGameStateChanged);
    }

    private void OnGameStateChanged(GameStateType newGameState)
    {
        Debug.Log($"Changing ui state to {newGameState}");

        joinUI.gameObject.SetActive(false);
        playUI.gameObject.SetActive(false);
        endUI.gameObject.SetActive(false);

        switch (newGameState)
        {
            case GameStateType.Join:
                joinUI.gameObject.SetActive(true);
                break;
            case GameStateType.Play:
                playUI.gameObject.SetActive(true);
                break;
            case GameStateType.End:
                endUI.gameObject.SetActive(true);
                break;
        }
    }
}
