using System;
public class DeliveryTable : BaseTable
{
    public static DeliveryTable Instance { get; private set; }
    public Action OnTryDeliverWithoutPlate;

    private void Awake()
    {
        Instance = this;
    }

    public override void Interact(Player player)
    {
        if (player.HasKitchenObject())
        {
            if(player.KitchenObject.TryGetPlate(out PlateKitchenObject plate))
            {
                DeliveryManager.Instance.DeliverRecipe(plate);
                player.KitchenObject.DestroySelf();
            }
            else
            {
                OnTryDeliverWithoutPlate?.Invoke();
            }
        }
    } 
}
