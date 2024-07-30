using UnityEngine;

public class KitchenObject : MonoBehaviour
{
    [SerializeField] private KitchenObjectSO kitchenObjectSO;

    private IKitchenObjectParent kitchenObjectParent;
    public IKitchenObjectParent KitchenObjectParent { get { return kitchenObjectParent; } 
        set
        {
            if(kitchenObjectParent != null)
                kitchenObjectParent.ClearKitchenObject();

            kitchenObjectParent = value;

            if (kitchenObjectParent.HasKitchenObject())
                Debug.Log("already has object with it");

            kitchenObjectParent.KitchenObject = this;

            transform.parent = kitchenObjectParent.GetKitchenObjectFollowTransform();
            transform.localPosition = Vector3.zero;
        }
    }

    public KitchenObjectSO GetKitchenObjectSO() => kitchenObjectSO;

    public void DestroySelf()
    {
        kitchenObjectParent.ClearKitchenObject();
        Destroy(gameObject);
    }

    public bool TryGetPlate(out PlateKitchenObject plateKitchenObject)
    {
        if(this is PlateKitchenObject)
        {
            plateKitchenObject = this as PlateKitchenObject;
            return true;
        }
        else
        {
            plateKitchenObject = null;
            return false; 
        }
    }

    public static KitchenObject SpawnKitchenObject(KitchenObjectSO kitchenObjectSO, IKitchenObjectParent parent)
    {
        var kitchenObjectTransform = Instantiate(kitchenObjectSO.prefab);
        var kitchenObject = kitchenObjectTransform.GetComponent<KitchenObject>();
        kitchenObject.KitchenObjectParent = parent;
        return kitchenObject;
    }
}
