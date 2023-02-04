using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayButton : MonoBehaviour
{
    private Button playStateButton;

    private CharactersManager charactersManager;

    private void Awake()
    {
        playStateButton = GetComponent<Button>();
        charactersManager = FindObjectOfType<CharactersManager>();
        playStateButton.interactable = false;
    }

    private void OnEnable()
    {
        charactersManager.OnPlayerAdded.AddListener(OnPlayerAdded);
        playStateButton.onClick.AddListener(OnProceedToPlay);
    }

    private void OnDisable()
    {
        charactersManager.OnPlayerAdded.RemoveListener(OnPlayerAdded);
        playStateButton.onClick.RemoveListener(OnProceedToPlay);
    }

    private void OnPlayerAdded(int playersAmount)
    {
        if (playersAmount > 1)
        {
            playStateButton.interactable = true;
        }
    }

    private void OnProceedToPlay()
    {
        GameManager.Singleton.GoToPlay();
    }
}
