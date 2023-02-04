using System;
using UnityEngine;
using UnityEngine.UI;

public class RestartButton : MonoBehaviour
{
    private Button _button;

    private void Awake()
    {
        _button = GetComponent<Button>();
    }

    private void OnEnable()
    {
        _button.onClick.AddListener(OnRestartClicked);
    }

    private void OnDisable()
    {
        _button.onClick.RemoveListener(OnRestartClicked);
    }

    private void OnRestartClicked()
    {
        GameManager.Singleton.Restart();
    }
}
