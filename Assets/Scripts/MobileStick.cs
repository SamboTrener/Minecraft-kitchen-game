using UnityEngine;

public class MobileStick : MonoBehaviour
{
    private void Awake()
    {
        if (!Application.isMobilePlatform)
        {
            Debug.Log(Application.isMobilePlatform);
            gameObject.SetActive(false);
        }
    }
}
