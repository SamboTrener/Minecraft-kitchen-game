using UnityEngine;

public class SwordAnimator : MonoBehaviour
{
    private const string CUT_TRIGGER = "Cut";

    private Animator animator;

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    private void Start()
    {
        CuttingTable.OnAnyCut += CuttingTable_OnAnyCut;
    }

    private void OnDestroy()
    {
        CuttingTable.OnAnyCut -= CuttingTable_OnAnyCut;
    }

    private void CuttingTable_OnAnyCut(object sender, System.EventArgs e)
    {
        animator.SetTrigger(CUT_TRIGGER);
    }
}
