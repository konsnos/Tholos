using UnityEngine;

public class UIStatesHandler : MonoBehaviour
{
    [SerializeField] private GameObject[] joinUIs;
    [SerializeField] private GameObject[] playUIs;
    [SerializeField] private GameObject[] endUIs;

    private void Awake()
    {
        GameManager.Singleton.OnStateChanged.AddListener(OnGameStateChanged);
    }

    private void OnGameStateChanged(GameStateType newGameState)
    {
        Debug.Log($"Changing ui state to {newGameState}");

        SetEnabledList(joinUIs, false);
        SetEnabledList(playUIs, false);
        SetEnabledList(endUIs, false);

        switch (newGameState)
        {
            case GameStateType.Join:
                SetEnabledList(joinUIs, true);
                break;
            case GameStateType.Play:
                SetEnabledList(playUIs, true);
                break;
            case GameStateType.End:
                SetEnabledList(endUIs, true);
                break;
        }
    }

    private void SetEnabledList(GameObject[] list, bool setEnabled)
    {
        foreach (var item in list)
        {
            item.SetActive(setEnabled);
        }
    }
}
