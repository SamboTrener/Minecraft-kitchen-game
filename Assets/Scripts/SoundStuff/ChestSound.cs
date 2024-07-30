using UnityEngine;

public class ChestSound : MonoBehaviour
{
    [SerializeField] Chest chest;
    AudioSource audioSource;

    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
    }

    private void Start()
    {
        chest.OnChestOpen += Chest_OnChestOpen;
    }

    private void Chest_OnChestOpen()
    {
        audioSource.Play();
    }
}
