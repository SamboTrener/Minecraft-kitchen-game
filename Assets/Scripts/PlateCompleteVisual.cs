using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlateCompleteVisual : MonoBehaviour
{
    [Serializable]
    public struct KitchenObjectSO_GameObject
    {
        public KitchenObjectSO KitchenObjectSO;
        public GameObject GameObject;
    }

    [SerializeField] PlateKitchenObject plateKitchenObject;
    [SerializeField] List<KitchenObjectSO_GameObject> kitchenObjectSO_GameObjectList;

    private void Start()
    {
        plateKitchenObject.OnIngridientAdded += PlateKitchenObject_OnIngridientAdded;
        foreach (var kitchenObjectSO_GameObject in kitchenObjectSO_GameObjectList)
        {
            kitchenObjectSO_GameObject.GameObject.SetActive(false);
        }
    }

    private void PlateKitchenObject_OnIngridientAdded(object sender, PlateKitchenObject.OnIngridientAddedEventArgs e)
    {
        foreach (var kitchenObjectSO_GameObject in kitchenObjectSO_GameObjectList)
        {
            if(kitchenObjectSO_GameObject.KitchenObjectSO == e.KitchenObjectSO)
            {
                kitchenObjectSO_GameObject.GameObject.SetActive(true);
            }
        }
    }
}
