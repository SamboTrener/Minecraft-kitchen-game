using System;
using UnityEngine;

public class BaseTable : MonoBehaviour, IKitchenObjectParent
{
    public static event EventHandler OnAnyObjectPlacedHere;

    public static void ResetStaticData()
    {
        OnAnyObjectPlacedHere = null;
    }

    [SerializeField] private Transform counterTopPoint;

    private KitchenObject kitchenObject;
    public KitchenObject KitchenObject
    {
        get => kitchenObject;
        set
        {
            kitchenObject = value;
            if (kitchenObject != null)
            {
                OnAnyObjectPlacedHere?.Invoke(this, EventArgs.Empty);
            }
        }
    }

    public Transform GetKitchenObjectFollowTransform() => counterTopPoint;

    public void ClearKitchenObject() => KitchenObject = null;

    public bool HasKitchenObject() => KitchenObject != null;

    public virtual void Interact(Player player)
    {
        Debug.LogError("Base Counter Interact");
    }
}
