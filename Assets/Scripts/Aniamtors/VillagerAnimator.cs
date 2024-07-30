using UnityEngine;

public class VillagerAnimator : MonoBehaviour
{
    private const string IS_VILLAGER_WALK = "IsVillagerWalk";


    private Animator animator;

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    private void Update()
    {
        animator.SetBool(IS_VILLAGER_WALK, Villager.Instance.IsVillagerComming || Villager.Instance.VillagerShouldLeave);
    }
}
