using System;
using UnityEngine;
using UnityEngine.UI;

public class TutorialUI : MonoBehaviour
{

    [SerializeField] Button playButton;
    public static event EventHandler OnResume;

    public static void ResetStaticData()
    {
        OnResume = null;
    }

    private void Awake()
    {
        playButton.onClick.AddListener(() =>
        {
            SaveLoadManager.FirstTimePlayChange();
            gameObject.SetActive(false);
            OnResume?.Invoke(this, EventArgs.Empty);
        });
        if(!SaveLoadManager.GetIsFirstTimePlayInfo())
        {
            gameObject.SetActive(false);
        }
    }
}
