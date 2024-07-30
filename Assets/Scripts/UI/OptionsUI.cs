using TMPro;
using UnityEngine;
using UnityEngine.UI;
using YG;

public class OptionsUI : MonoBehaviour
{
    public static OptionsUI Instance { get; private set; }

    [SerializeField] Button soundEffectsButton;
    [SerializeField] Button musicButton;
    [SerializeField] Button exitButton;
    [SerializeField] TextMeshProUGUI soundEffectsText;
    [SerializeField] TextMeshProUGUI musicText;

    private void Awake()
    {
        Instance = this;
        soundEffectsButton.onClick.AddListener(() =>
        {
            SoundManager.Instance.ChangeVolume();
            UpdateVisual();
        });
        musicButton.onClick.AddListener(() =>
        {
            MusicManager.Instance.ChangeVolume();
            UpdateVisual();
        });
        exitButton.onClick.AddListener(() =>
        {
            gameObject.SetActive(false);
        });
        gameObject.SetActive(false);
    }

    private void Start()
    {
        UpdateVisual();
    }

    void UpdateVisual()
    {
        if (YandexGame.EnvironmentData.language == "ru")
        {
            soundEffectsText.text = $"Громкость звуков: {SoundManager.Instance.GetVolumeNormalized()}";
            musicText.text = $"Громкость музыки: {MusicManager.Instance.GetVolumeNormalized()}";
        }
        else
        {
            soundEffectsText.text = $"Sound Effects: {SoundManager.Instance.GetVolumeNormalized()}";
            musicText.text = $"Music: {MusicManager.Instance.GetVolumeNormalized()}";
        }
    }
}
