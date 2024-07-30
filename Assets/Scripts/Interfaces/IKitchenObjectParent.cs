using UnityEngine;

public interface IKitchenObjectParent
{
    public KitchenObject KitchenObject { get; set; }

    public Transform GetKitchenObjectFollowTransform();

    public void ClearKitchenObject();

    public bool HasKitchenObject();
}
