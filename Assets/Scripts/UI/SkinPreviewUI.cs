using System;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using YG;

public class SkinPreviewUI : MonoBehaviour
{
    public static SkinPreviewUI Instance { get; private set;}
    [SerializeField] Image magshot;
    [SerializeField] TextMeshProUGUI speed;
    [SerializeField] TextMeshProUGUI cost;
    [SerializeField] Button purchaseButton;
    [SerializeField] TextMeshProUGUI buttonText;
    [SerializeField] GameObject errorWindow;
    SkinSO currentSkin;
    public Action OnSkinPicked;

    private void Awake()
    {
        Instance = this;
        purchaseButton.onClick.AddListener(PurchaseSkin);
    }
    private void Start()
    {
        SkinsUI.Instance.OnSkinPicked += SkinsUI_OnSkinPicked;
        gameObject.SetActive(false);
    }

    private void PurchaseSkin()
    {
        var isSkinPurchased = SaveLoadManager.TryPurchaseSkin(currentSkin);
        if (!isSkinPurchased)
        {
            StartCoroutine(ShowErrorWindow());
        }
        else
        {
            MetricaSender.SendPurchasedSkinName(currentSkin.Name);
            YandexGame.ReviewShow(false);
        }
        CorrectPurchaseButton(currentSkin);
    }

    IEnumerator ShowErrorWindow()
    {
        errorWindow.SetActive(true);
        yield return new WaitForSeconds(1f);
        errorWindow.SetActive(false);
    }

    private void SkinsUI_OnSkinPicked(SkinSO skinSO)
    {
        Debug.Log("OnSkinPicked cathed");
        CorrectPurchaseButton(skinSO);
        currentSkin = skinSO;
        gameObject.SetActive(true);
        magshot.sprite = skinSO.Magshot;

        if (YandexGame.EnvironmentData.language == "ru")
        {
            speed.text = $"Скорость: {Math.Round(skinSO.SpeedMultiplier * 28)}";
            cost.text = $"Цена: {skinSO.Cost}";
        }
        else
        {
            speed.text = $"Speed: {skinSO.SpeedMultiplier}";
            cost.text = $"Cost: {skinSO.Cost}";
        }
        CorrectPurchaseButton(skinSO);
    }

    private void CorrectPurchaseButton(SkinSO skinSO)
    {
        if (SaveLoadManager.IsSkinPurchased(skinSO))
        {
            purchaseButton.onClick.RemoveAllListeners();
            if (skinSO.Name == SaveLoadManager.GetCurrentSkinName())
            {
                if (YandexGame.EnvironmentData.language == "ru")
                {
                    buttonText.text = "Надет";
                }
                else
                {
                    buttonText.text = "Taken";
                }
                buttonText.color = Color.green;
            }
            else
            {
                purchaseButton.onClick.AddListener(() =>
                {
                    SaveLoadManager.SaveCurrentSkinName(currentSkin.Name);
                    OnSkinPicked?.Invoke();
                });
                if(YandexGame.EnvironmentData.language == "ru")
                {
                    buttonText.text = "Надеть";
                }
                else
                {
                    buttonText.text = "Take this";
                }
                buttonText.color = Color.green;
            }
        }
        else
        {
            purchaseButton.onClick.RemoveAllListeners();
            purchaseButton.onClick.AddListener(PurchaseSkin);

            if (YandexGame.EnvironmentData.language == "ru")
            {
                buttonText.text = "Купить";
            }
            else
            {
                buttonText.text = "Purchase";
            }
            buttonText.color = new Color(245, 0, 149, 255);
        }
    }
}
