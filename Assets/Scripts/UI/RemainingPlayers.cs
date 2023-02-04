using System.Text;
using TMPro;
using UnityEngine;

public class RemainingPlayers : MonoBehaviour
{
    private TMP_Text _remainingPlayersText;
    private CharactersManager _charactersManager;

    private void Awake()
    {
        _remainingPlayersText = GetComponent<TMP_Text>();
        _charactersManager = FindObjectOfType<CharactersManager>();
    }

    private void OnEnable()
    {
        _charactersManager.OnCharacterRemoved.AddListener(OnCharacterRemoved);
        UpdatePlayers();
    }

    private void OnDisable()
    {
        _charactersManager.OnCharacterRemoved.RemoveListener(OnCharacterRemoved);
    }

    private void UpdatePlayers()
    {
        var stringBuilder = new StringBuilder();

        if(_charactersManager.Characters.Count == 0)
        {
            stringBuilder.AppendLine("Evil potato wins!");
        }
        else
        {
            foreach (var item in _charactersManager.Characters)
            {
                stringBuilder.AppendLine($"Player {item.Value.CharacterId}");
            }
        }

        _remainingPlayersText.text = stringBuilder.ToString();
    }

    private void OnCharacterRemoved()
    {
        UpdatePlayers();
    }
}
