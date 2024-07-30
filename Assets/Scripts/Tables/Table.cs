using System;

public class Table : BaseTable
{
    public static event EventHandler OnTryPutSecondObject;

    new public static void ResetStaticData()
    {
        OnTryPutSecondObject = null;
    }

    public override void Interact(Player player)
    {
        if (!HasKitchenObject())
        {
            if (player.HasKitchenObject())
            {
                player.KitchenObject.KitchenObjectParent = this;
            }
        }
        else
        {
            if (!player.HasKitchenObject())
            {
                KitchenObject.KitchenObjectParent = player;
            }
            else
            {
                if (player.KitchenObject.TryGetPlate(out var plateKitchenObject))
                {
                    if (plateKitchenObject.TryAddIngredient(KitchenObject.GetKitchenObjectSO()))
                    {
                        KitchenObject.DestroySelf();
                    }
                }
                else if (KitchenObject.TryGetPlate(out plateKitchenObject))
                {
                    if (plateKitchenObject.TryAddIngredient(player.KitchenObject.GetKitchenObjectSO()))
                    {
                        player.KitchenObject.DestroySelf();
                    }
                }
                else
                {
                    OnTryPutSecondObject?.Invoke(this, EventArgs.Empty);
                }
            }
        }
    }
}
