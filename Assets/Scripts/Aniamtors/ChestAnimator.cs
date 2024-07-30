using UnityEngine;

public class ChestAnimator : MonoBehaviour
{
    private const string OPEN_CHEST = "Open_Chest";

    [SerializeField] Chest chest;

    private Animator animator;

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    void Start()
    {
        chest.OnChestOpen += Chest_OnChestOpen;
    }

    private void Chest_OnChestOpen()
    {
        animator.SetTrigger(OPEN_CHEST);
    }
}
