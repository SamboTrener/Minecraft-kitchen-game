using UnityEngine;
using UnityEngine.UI;

public class MainMenuUI : MonoBehaviour
{
    [SerializeField] Button playButton;
    [SerializeField] Button skinButton;
    [SerializeField] SideMenuUI skinMenuUI;

    private void Awake()
    {
        playButton.onClick.AddListener(() => Loader.Load(Loader.Scene.GameScene));
        skinButton.onClick.AddListener(() =>
        {
            skinMenuUI.gameObject.SetActive(true);
            SkinPreviewUI.Instance.gameObject.SetActive(false);
        });

        Time.timeScale = 1f;
    }
}
