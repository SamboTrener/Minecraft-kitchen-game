using TMPro;
using UnityEngine;

public class MoneyCountTextChanger : MonoBehaviour
{
    private void Awake()
    {
        MoneyChangedVisual(SaveLoadManager.GetMoneyData());
        SaveLoadManager.OnMoneyChangedVisual += MoneyChangedVisual;
    }

    private void OnDestroy()
    {
        SaveLoadManager.OnMoneyChangedVisual -= MoneyChangedVisual;
    }

    void MoneyChangedVisual(int money)
    {
        gameObject.GetComponent<TextMeshProUGUI>().text = money.ToString();
    }
}
