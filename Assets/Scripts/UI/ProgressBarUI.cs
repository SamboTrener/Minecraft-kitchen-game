using UnityEngine;
using UnityEngine.UI;

public class ProgressBarUI : MonoBehaviour
{
    [SerializeField] GameObject hasProgressGO;
    [SerializeField] Image barImage;

    IHaveProgress hasProgress;

    private void Start()
    {
        hasProgress = hasProgressGO.GetComponent<IHaveProgress>();
        if(hasProgress == null)
            Debug.Log("Game object does not have a component IHaveProgress");

        hasProgress.OnProgressChanged += ShowProgressChange;
        barImage.fillAmount = 0f;
        gameObject.SetActive(false);
    }

    private void ShowProgressChange(object sender, IHaveProgress.OnProgressChangedEventArgs e)
    {
        barImage.fillAmount = e.progressNormalized;
        if(e.progressNormalized == 0f ||  e.progressNormalized == 1f)
            gameObject.SetActive(false);
        else
            gameObject.SetActive(true);
    }
}
