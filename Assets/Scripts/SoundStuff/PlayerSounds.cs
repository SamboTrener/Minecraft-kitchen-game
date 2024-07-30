using UnityEngine;

public class PlayerSounds : MonoBehaviour
{
    Player player;
    float footstepTimer;
    float footstepTimerMax = 0.5f;

    private void Awake()
    {
        player = GetComponent<Player>();
    }

    private void Update()
    {
        footstepTimer -= Time.deltaTime;
        if( footstepTimer < 0f ) 
        {
            footstepTimer = footstepTimerMax;

            if (player.IsWalking)
            {
                var volume = 1f;
                SoundManager.Instance.PlayFootstepsSound(player.transform.position, volume);
            }
        }
    }
}
