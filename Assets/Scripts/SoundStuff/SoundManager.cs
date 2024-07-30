using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public static SoundManager Instance;

    [SerializeField] private AudioClipRefsSO audioClipRefsSO;

    float volume = 1f;

    private void Awake()
    {
        Instance = this;
    }

    private void PauseGameUI_OnOptionsOpened(object sender, System.EventArgs e)
    {
        if(SaveLoadManager.GetSoundVolume() > 0)
        {
            volume = SaveLoadManager.GetSoundVolume();
        }
    }

    private void Start()
    {
        DeliveryManager.Instance.OnRecipeSuccess += DeliveryManager_OnRecipeSuccess;
        DeliveryManager.Instance.OnRecipeFailed += DeliveryManager_OnRecipeFailed;
        CuttingTable.OnAnyCut += CuttingTable_OnAnyCut;
        Player.Instance.OnPickedSomething += Player_OnPickedSomething;
        BaseTable.OnAnyObjectPlacedHere += BaseTable_OnAnyObjectPlacedHere;
        Trash.OnAnyObjectTrashed += Trash_OnAnyObjectTrashed;
        PauseGameUI.Instance.OnOptionsOpened += PauseGameUI_OnOptionsOpened;
    }

    private void Trash_OnAnyObjectTrashed(object sender, System.EventArgs e)
    {
        var trash = sender as Trash;
        PlaySound(audioClipRefsSO.trash, trash.transform.position);
    }

    private void BaseTable_OnAnyObjectPlacedHere(object sender, System.EventArgs e)
    {
        var baseTable = sender as BaseTable;
        PlaySound(audioClipRefsSO.objectDrop, baseTable.transform.position);
    }

    private void Player_OnPickedSomething(object sender, System.EventArgs e)
    {
        PlaySound(audioClipRefsSO.objectPickup, Player.Instance.transform.position);
    }

    private void CuttingTable_OnAnyCut(object sender, System.EventArgs e)
    {
        var cuttingTable = sender as CuttingTable;
        PlaySound(audioClipRefsSO.chop, cuttingTable.transform.position);
    }

    private void DeliveryManager_OnRecipeFailed(object sender, System.EventArgs e)
    {
        var deliveryTable = DeliveryTable.Instance;
        PlaySound(audioClipRefsSO.deliveryFail, deliveryTable.transform.position);
    }

    private void DeliveryManager_OnRecipeSuccess(object sender, System.EventArgs e)
    {
        var deliveryTable = DeliveryTable.Instance;
        PlaySound(audioClipRefsSO.deliverySuccess, deliveryTable.transform.position);
    }

    public void PlayFootstepsSound(Vector3 position, float volume)
    {
        PlaySound(audioClipRefsSO.footstep, position, volume);
    }

    private void PlaySound(AudioClip[] audioClipArray, Vector3 position, float volume = 1f)
    {
        PlaySound(audioClipArray[Random.Range(0, audioClipArray.Length)], position, volume);
    }

    private void PlaySound(AudioClip audioClip, Vector3 position, float volumeMultiplier = 1f)
    {
        AudioSource.PlayClipAtPoint(audioClip, position, volumeMultiplier * volume);
    }

    public void ChangeVolume()
    {
        volume += .1f;
        if (volume >= 1.1f)
        {
            volume = 0f;
        }
        SaveLoadManager.SaveSoundVolume(volume);
    }

    public float GetVolumeNormalized() => Mathf.Round(volume * 10);
}
