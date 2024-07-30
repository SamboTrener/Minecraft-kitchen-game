using UnityEngine;

public class MusicManager : MonoBehaviour
{
    public static MusicManager Instance { get; private set; }

    AudioSource audioSource;
    float volume = .5f;
    float LoudnessCoef = 0.1f;

    private void Awake()
    {
        Instance = this;
        audioSource = GetComponent<AudioSource>();
    }

    private void Start()
    {
        PauseGameUI.Instance.OnOptionsOpened += PauseGameUI_OnOptionsOpened;
    }

    private void PauseGameUI_OnOptionsOpened(object sender, System.EventArgs e)
    {
        if(SaveLoadManager.GetMusicVolume() > 0)
        {
            volume = SaveLoadManager.GetMusicVolume();
        }
    }

    public void ChangeVolume()
    {
        volume += .1f;
        if (volume >= 1.1)
        {
            volume = 0f;
        }
        audioSource.volume = volume * LoudnessCoef;
        SaveLoadManager.SaveMusicVolume(volume);
    }

    public float GetVolumeNormalized() => Mathf.Round(volume * 10);
}
