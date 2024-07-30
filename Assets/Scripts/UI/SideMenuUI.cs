using UnityEngine;
using UnityEngine.UI;

public class SideMenuUI : MonoBehaviour
{
    [SerializeField] Button closeButton;
    [SerializeField] GameObject errorWindow;

    private void Awake()
    {
        closeButton.onClick.AddListener(() =>
        {
            errorWindow.SetActive(false);
            gameObject.SetActive(false);
        });
    }

    private void Start()
    {
        SkinPreviewUI.Instance.OnSkinPicked += () => gameObject.SetActive(false);
        gameObject.SetActive(false);
    }
}
