using System.Collections;
using TMPro;
using UnityEngine;
using YG;

public class PlayerErrorHandler : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI errorText;
    private void OnDestroy()
    {
        Furnace.OnTryGiveInvalidInput -= Furnace_OnTryGiveInvalidInput;
        Chest.OnTryTakeSecondObject -= Chest_OnTryTakeSecondObject;
        CuttingTable.OnTryCutInvalidObject -= CuttingTable_OnTryCutInvalidObject;

        CuttingTable.OnTryPutSecondObject -= Table_OnTryPutSecondObject;
        Furnace.OnTryPutSecondObject -= Table_OnTryPutSecondObject;
        Table.OnTryPutSecondObject -= Table_OnTryPutSecondObject;

        PlateKitchenObject.OnTryAddInvalidIngridient -= PlateKitchenObject_OnTryAddInvalidIngridient;
        PlateKitchenObject.OnTryAddSameIngridient -= PlateKitchenObject_OnTryAddSameIngridient;
    }

    private void Start()
    {
        Furnace.OnTryGiveInvalidInput += Furnace_OnTryGiveInvalidInput;
        DeliveryTable.Instance.OnTryDeliverWithoutPlate += DeliveryTable_OnTryDeliverWithoutPlate;
        Chest.OnTryTakeSecondObject += Chest_OnTryTakeSecondObject;
        CuttingTable.OnTryCutInvalidObject += CuttingTable_OnTryCutInvalidObject;

        CuttingTable.OnTryPutSecondObject += Table_OnTryPutSecondObject;
        Furnace.OnTryPutSecondObject += Table_OnTryPutSecondObject;
        Table.OnTryPutSecondObject += Table_OnTryPutSecondObject;

        PlateKitchenObject.OnTryAddInvalidIngridient += PlateKitchenObject_OnTryAddInvalidIngridient;
        PlateKitchenObject.OnTryAddSameIngridient += PlateKitchenObject_OnTryAddSameIngridient;
        gameObject.SetActive(false);
    }


    private void PlateKitchenObject_OnTryAddInvalidIngridient()
    {
        if (YandexGame.EnvironmentData.language == "ru")
        {
            ShowMessage("��� �� ����� ��� �� �������");
        }
        else
        {
            ShowMessage("I don't need to take it on plate");
        }
        StartCoroutine(DeactivateWindow());
    }

    private void PlateKitchenObject_OnTryAddSameIngridient()
    {
        if (YandexGame.EnvironmentData.language == "ru")
        {
            ShowMessage("� ���� ��� ���� ��� �� �������");
        }
        else
        {
            ShowMessage("I already have this on my plate");
        }
        StartCoroutine(DeactivateWindow());
    }

    private void Table_OnTryPutSecondObject(object sender, System.EventArgs e)
    {
        if (YandexGame.EnvironmentData.language == "ru")
        {
            ShowMessage("����� ��� ���-�� �����");
        }
        else
        {
            ShowMessage("There's already something lying here");
        }
        StartCoroutine(DeactivateWindow());
    }

    private void CuttingTable_OnTryCutInvalidObject()
    {
        if (YandexGame.EnvironmentData.language == "ru")
        {
            ShowMessage("� �� ���� ��� ��������. ����� ������� ��������?");
        }
        else
        {
            ShowMessage("I can't cut it. Maybe need to roast first?");
        }
        StartCoroutine(DeactivateWindow());
    }

    private void Chest_OnTryTakeSecondObject()
    {
        if (YandexGame.EnvironmentData.language == "ru")
        {
            ShowMessage("� ���� ������ ����");
        }
        else
        {
            ShowMessage("I already have something in my hands");
        }
        StartCoroutine(DeactivateWindow());
    }


    void DeliveryTable_OnTryDeliverWithoutPlate()
    {
        if (YandexGame.EnvironmentData.language == "ru")
        {
            ShowMessage("� �� ���� ������ ����� ��� �������");
        }
        else
        {
            ShowMessage("I can't deliver without a plate");
        }
        StartCoroutine(DeactivateWindow());
    }

    private void Furnace_OnTryGiveInvalidInput(object sender, System.EventArgs e)
    {
        if (YandexGame.EnvironmentData.language == "ru")
        {
            ShowMessage("� �� ���� ��� ��������");
        }
        else
        {
            ShowMessage("I can't roast it");
        }
        StartCoroutine(DeactivateWindow());
    }

    void ShowMessage(string message)
    {
        gameObject.SetActive(true);
        errorText.text = message;
    }

    IEnumerator DeactivateWindow()
    {
        yield return new WaitForSeconds(1f);
        gameObject.SetActive(false);
    }
}
