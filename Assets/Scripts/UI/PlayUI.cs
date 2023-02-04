using System;
using TMPro;
using UnityEngine;

public class PlayUI : MonoBehaviour
{
    [SerializeField] private TMP_Text _timerText;

    private PlayState _playState;

    private void Awake()
    {
        _playState = FindObjectOfType<PlayState>();
    }

    private void Update()
    {
        _timerText.text = $"Remaining time: {_playState.SecondsRemaining}";
    }
}
