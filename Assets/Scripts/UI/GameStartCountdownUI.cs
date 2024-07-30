using TMPro;
using UnityEngine;

public class GameStartCountdownUI : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI countdownText;

    private void Start()
    {
        KitchenGameManager.Instance.OnStateChanged += KitchenGameManager_OnStateChenged;

        gameObject.SetActive(false);
    }

    private void KitchenGameManager_OnStateChenged(object sender, System.EventArgs e)
    {
        if (KitchenGameManager.Instance.IsCountdownToStartActive())
            gameObject.SetActive(true);
        else
            gameObject.SetActive(false);
    }

    private void Update()
    {
        countdownText.text = Mathf.Ceil(KitchenGameManager.Instance.CountdownToStartTimer).ToString();
    }
}
