using System;
using UnityEngine;

public class Chest : BaseTable
{
    [SerializeField] KitchenObjectSO kitchenObjectSO;
    [SerializeField] SpriteRenderer spriteRenderer;

    public Action OnChestOpen;
    public static Action OnTryTakeSecondObject;
    new public static void ResetStaticData()
    {
        OnTryTakeSecondObject = null;
    }

    private void OnEnable()
    {
        spriteRenderer.sprite = kitchenObjectSO.sprite;
    }

    public override void Interact(Player player)
    {
        if (player.HasKitchenObject())
        {
            if (player.KitchenObject.TryGetPlate(out var plateKitchenObject))
            {
                if(plateKitchenObject.TryAddIngredient(kitchenObjectSO))
                {
                    OnChestOpen?.Invoke();
                }
            }
            else
            {
                OnTryTakeSecondObject?.Invoke();
            }
        }
        else
        {
            KitchenObject.SpawnKitchenObject(kitchenObjectSO, player);
            OnChestOpen?.Invoke();
        }
    }
}
