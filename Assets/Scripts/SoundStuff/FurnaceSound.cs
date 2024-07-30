using UnityEngine;

public class FurnaceSound : MonoBehaviour
{
    [SerializeField] Furnace furnace;
    AudioSource audioSource;

    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
    }

    private void Start()
    {
        furnace.OnFurnaceCleared += Furnace_OnFurnaceCleared;
        furnace.OnFurnaceOccupied += Furnace_OnFurnaceOccupied;
    }

    private void Furnace_OnFurnaceOccupied(object sender, System.EventArgs e)
    {
        audioSource.Play();
    }

    private void Furnace_OnFurnaceCleared(object sender, System.EventArgs e)
    {
        audioSource.Pause();
    }
}
