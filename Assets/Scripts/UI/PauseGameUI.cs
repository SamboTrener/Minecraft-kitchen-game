using System;
using UnityEngine;
using UnityEngine.UI;

public class PauseGameUI : MonoBehaviour
{
    public static PauseGameUI Instance { get; private set; }
    public event EventHandler OnResume;
    public event EventHandler OnOptionsOpened;

    [SerializeField] Button resumeButton;
    [SerializeField] Button optionsButton;
    [SerializeField] Button mainMenuButton;
    [SerializeField] Button tutorialButton;
    [SerializeField] GameObject tutorialMenu;

    private void Awake()
    {
        Instance = this;
        resumeButton.onClick.AddListener(() =>
        {
            gameObject.SetActive(false);
            OnResume?.Invoke(this, EventArgs.Empty);
        });
        mainMenuButton.onClick.AddListener(() => Loader.Load(Loader.Scene.MainMenuScene));
        optionsButton.onClick.AddListener(() =>
        {
            OnOptionsOpened?.Invoke(this, EventArgs.Empty);
            OptionsUI.Instance.gameObject.SetActive(true);
        });
        tutorialButton.onClick.AddListener(() => 
        { 
            tutorialMenu.SetActive(true);
            gameObject.SetActive(false);
        });
        gameObject.SetActive(false);
    }
}
