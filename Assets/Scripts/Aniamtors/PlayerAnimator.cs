using UnityEngine;

public class PlayerAnimator : MonoBehaviour
{
    private const string IS_WALKING = "IsWalking";
    private const string IS_PICKED_SOMETHING = "IsPickedSomethingTrigger";
    private const string IS_DROP_SOMETHING = "IsDropSomethingTrigger";

    private Animator animator;

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    private void Start()
    {
        Player.Instance.OnPickedSomething += Player_OnPickedSomething;
        Player.Instance.OnDropSomething += Player_OnDropSomething;
    }

    private void Player_OnDropSomething(object sender, System.EventArgs e)
    {
        animator.SetTrigger(IS_DROP_SOMETHING);
    }

    private void Player_OnPickedSomething(object sender, System.EventArgs e)
    {
        animator.SetTrigger(IS_PICKED_SOMETHING);
    }

    private void Update()
    {
        animator.SetBool(IS_WALKING, Player.Instance.IsWalking);
    }
}
