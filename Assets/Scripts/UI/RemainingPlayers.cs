using System.Collections;
using System.Collections.Generic;
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

    private void Start()
    {
        var stringBuilder = new StringBuilder();

        foreach (var item in _charactersManager.Characters)
        {
            stringBuilder.AppendLine($"Player {item.Value.CharacterId}");
        }

        _remainingPlayersText.text = stringBuilder.ToString();
    }
}
