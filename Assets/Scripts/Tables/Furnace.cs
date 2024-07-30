using System;
using UnityEngine;

public class Furnace : BaseTable, IHaveProgress
{

    [SerializeField] RoastingRecipeSO[] roastingRecipeSOArray;

    float roastingTimer;
    RoastingRecipeSO roastingRecipeSO;

    public event EventHandler<IHaveProgress.OnProgressChangedEventArgs> OnProgressChanged;
    public event EventHandler OnFurnaceCleared;
    public event EventHandler OnFurnaceOccupied;
    public static event EventHandler OnTryGiveInvalidInput;
    public static event EventHandler OnTryPutSecondObject;

    new public static void ResetStaticData()
    {
        OnTryGiveInvalidInput = null;
        OnTryPutSecondObject = null;
    }

    private void Update()
    {
        if (HasKitchenObject() && HasRecipeWithInput(KitchenObject.GetKitchenObjectSO()))
        {
            roastingTimer += Time.deltaTime;
            OnProgressChanged?.Invoke(this, new IHaveProgress.OnProgressChangedEventArgs { progressNormalized = roastingTimer / roastingRecipeSO.roastingTimerMax });
            if (roastingTimer > roastingRecipeSO.roastingTimerMax)
            {
                KitchenObject.DestroySelf();
                KitchenObject.SpawnKitchenObject(roastingRecipeSO.Output, this);
                HideBarEventInvoke();
            }
            roastingRecipeSO = GetRoastingRecipeSOWithInput(KitchenObject.GetKitchenObjectSO()); //Пересчитываю инпут
        }
    }

    public override void Interact(Player player)
    {
        if (HasKitchenObject()) //в печи объект
        {
            if (HasRecipeWithOutput(KitchenObject.GetKitchenObjectSO())) //дожарен 
            {
                if (!player.HasKitchenObject()) //у плеера в руках пусто
                {
                    KitchenObject.KitchenObjectParent = player;
                    OnFurnaceCleared?.Invoke(this, EventArgs.Empty); //Объект забрал в руки плеер
                    HideBarEventInvoke();
                }
                else if (player.KitchenObject.TryGetPlate(out var plateKitchenObject)) //у плеера в руках тарелка
                {
                    if (plateKitchenObject.TryAddIngredient(KitchenObject.GetKitchenObjectSO())) //В тарелке ещё нет такого ингредиента
                    {
                        KitchenObject.DestroySelf();
                        OnFurnaceCleared?.Invoke(this, EventArgs.Empty); //Объект забрал плеер на тарелку
                        HideBarEventInvoke();
                    }
                }
                else
                {
                    OnTryPutSecondObject?.Invoke(this, EventArgs.Empty);
                }
            }
        }
        else if (player.HasKitchenObject())
        {
            if (HasRecipeWithInput(player.KitchenObject.GetKitchenObjectSO()))
            {
                player.KitchenObject.KitchenObjectParent = this;
                OnFurnaceOccupied?.Invoke(this, EventArgs.Empty); //Объект положили на печь
                roastingRecipeSO = GetRoastingRecipeSOWithInput(KitchenObject.GetKitchenObjectSO());
                HideBarEventInvoke();
            }
            else
            {
                OnTryGiveInvalidInput?.Invoke(this, EventArgs.Empty);
            }
        }
    }

    private void HideBarEventInvoke()
    {
        roastingTimer = 0f;
        if (roastingRecipeSO != null) // Если после пересчёта вещь фул сгорела
            OnProgressChanged?.Invoke(this, new IHaveProgress.OnProgressChangedEventArgs { progressNormalized = roastingTimer / roastingRecipeSO.roastingTimerMax });
        else
            OnProgressChanged?.Invoke(this, new IHaveProgress.OnProgressChangedEventArgs { progressNormalized = 0 });
    }

    private bool HasRecipeWithInput(KitchenObjectSO input)
    {
        var recipe = GetRoastingRecipeSOWithInput(input);
        return recipe != null;
    }

    private bool HasRecipeWithOutput(KitchenObjectSO output)
    {
        var recipe = GetRoastingRecipeSOWithOutput(output);
        return recipe != null;
    }

    private RoastingRecipeSO GetRoastingRecipeSOWithInput(KitchenObjectSO input)
    {
        foreach (var recipe in roastingRecipeSOArray)
        {
            if (recipe.Input == input)
                return recipe;
        }
        return null;
    }

    private RoastingRecipeSO GetRoastingRecipeSOWithOutput(KitchenObjectSO output)
    {
        foreach (var recipe in roastingRecipeSOArray)
        {
            if (recipe.Output == output)
            {
                return recipe;
            }
        }
        return null;
    }
}
