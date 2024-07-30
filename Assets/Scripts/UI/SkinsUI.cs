using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using YG;

public class SkinsUI : MonoBehaviour
{
    public static SkinsUI Instance { get; private set; }

    [Serializable]
    public struct SkinSO_Button
    {
        public SkinSO skinSO;
        public Button button;
    }
    List<SkinSO_Button> skinSO_Buttons = new List<SkinSO_Button>();

    [SerializeField] List<Button> buttons;
    [SerializeField] List<Image> images;
    [SerializeField] List<TextMeshProUGUI> names;
    [SerializeField] SkinListSO skinList;

    public Action<SkinSO> OnSkinPicked;

    private void Awake()
    {
        Instance = this;
        for (int i = 0; i < skinList.skinSOList.Count; i++)
        {
            images[i].sprite = skinList.skinSOList[i].Magshot;
            if (YandexGame.EnvironmentData.language == "ru")
            {
                names[i].text = skinList.skinSOList[i].NameRussian;
            }
            else
            {
                names[i].text = skinList.skinSOList[i].Name;
            }
            skinSO_Buttons.Add(
            new SkinSO_Button()
            {
                skinSO = skinList.skinSOList[i],
                button = buttons[i]
            });
        }
        foreach (var element in skinSO_Buttons)
        {
            element.button.onClick.AddListener(() => OnSkinPicked?.Invoke(element.skinSO));
        }
    }
}
