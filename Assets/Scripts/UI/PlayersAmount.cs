using System;
using TMPro;
using UnityEngine;

public class PlayersAmount : MonoBehaviour
{
    private TMP_Text text;
    private CharactersManager charactersManager;

    private void Awake()
    {
        text = GetComponent<TMP_Text>();
        charactersManager = FindObjectOfType<CharactersManager>();
    }

    private void OnEnable()
    {
        charactersManager.OnCharacterAdded.AddListener(OnPlayerAdded);
    }

    private void OnDisable()
    {
        charactersManager.OnCharacterAdded.RemoveListener(OnPlayerAdded);
    }

    private void OnPlayerAdded(int playersAmount)
    {
        text.text = $"Players amount: {playersAmount}";
    }
}
