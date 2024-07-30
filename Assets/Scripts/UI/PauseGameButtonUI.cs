using System;
using UnityEngine;
using UnityEngine.UI;

public class PauseGameButtonUI : MonoBehaviour
{
    public static PauseGameButtonUI Instance { get; private set; }

    [SerializeField] Button pauseButton;
    [SerializeField] GameObject pauseGameUI;

    public event EventHandler OnPause;

    private void Awake()
    {
        Instance = this;
        pauseButton.onClick.AddListener(HandleButtonClick);
    }

    void HandleButtonClick()
    {
        OnPause?.Invoke(this, EventArgs.Empty);
        pauseGameUI.SetActive(true);
    }
}
