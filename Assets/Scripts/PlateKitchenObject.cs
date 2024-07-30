using System;
using System.Collections.Generic;
using UnityEngine;

public class PlateKitchenObject : KitchenObject
{
    public event EventHandler<OnIngridientAddedEventArgs> OnIngridientAdded;
    public class OnIngridientAddedEventArgs : EventArgs
    {
        public KitchenObjectSO KitchenObjectSO;
    }

    [SerializeField] List<KitchenObjectSO> validKitchenObjectSOList = new List<KitchenObjectSO>();

    List<KitchenObjectSO> kitchenObjectSOList = new List<KitchenObjectSO>();

    public static Action OnTryAddSameIngridient;
    public static Action OnTryAddInvalidIngridient;
    public static void ResetStaticData()
    {
        OnTryAddSameIngridient = null;
        OnTryAddInvalidIngridient = null;

    }

    public bool TryAddIngredient(KitchenObjectSO kitchenObjectSO)
    {
        if (kitchenObjectSOList.Contains(kitchenObjectSO))
        {
            OnTryAddSameIngridient?.Invoke();
            return false;
        }
        else if(!validKitchenObjectSOList.Contains(kitchenObjectSO))
        {
            OnTryAddInvalidIngridient?.Invoke();
            return false;
        }
        else
        {
            kitchenObjectSOList.Add(kitchenObjectSO);
            OnIngridientAdded?.Invoke(this, new OnIngridientAddedEventArgs { KitchenObjectSO = kitchenObjectSO });

            return true;
        }
    }

    public List<KitchenObjectSO> GetKitchenObjectSOList() => kitchenObjectSOList;
}
