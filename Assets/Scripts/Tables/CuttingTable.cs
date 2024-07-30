using System;
using UnityEngine;

public class CuttingTable : BaseTable, IHaveProgress
{
    public event EventHandler<IHaveProgress.OnProgressChangedEventArgs> OnProgressChanged;
    public static event EventHandler OnAnyCut;
    public static Action OnTryCutInvalidObject;
    public static event EventHandler OnTryPutSecondObject;

    new public static void ResetStaticData()
    {
        OnTryCutInvalidObject = null;
        OnAnyCut = null;
        OnTryPutSecondObject = null;
    }

    [SerializeField] CuttingRecipeSO[] cuttingRecipeSOArray;

    private int cuttingProgress;

    public override void Interact(Player player)
    {
        if (HasKitchenObject()) //Объект на столе 
        {
            if (HasRecipeWithOutput(KitchenObject.GetKitchenObjectSO())) //Объект дорезан
            {
                if (player.HasKitchenObject()) //У игрока в руках объект
                {
                    if (player.KitchenObject.TryGetPlate(out var plate)) //Это тарелка
                    {
                        if (plate.TryAddIngredient(KitchenObject.GetKitchenObjectSO())) //В тарелке ещё нет такого ингредиента
                        {
                            KitchenObject.DestroySelf();
                        }
                    }
                    else
                    {
                        OnTryPutSecondObject?.Invoke(this, EventArgs.Empty);
                    }
                }
                else //У игрока в руках пусто
                {
                    KitchenObject.KitchenObjectParent = player;
                    cuttingProgress = 0; //Обнуление прогресса когда предмет забирают
                }
            }
            else //Объект не дорезан
            {
                var progressNormalized = CutAndGetProgressNormalized();
                OnProgressChanged?.Invoke(this, new IHaveProgress.OnProgressChangedEventArgs { progressNormalized = progressNormalized });
            }
        }
        else if (player.HasKitchenObject()) 
        {
            if (HasRecipeWithInput(player.KitchenObject.GetKitchenObjectSO()))
            {
                player.KitchenObject.KitchenObjectParent = this;
            }
            else
            {
                OnTryCutInvalidObject?.Invoke();
            }
        }
    }

    public float CutAndGetProgressNormalized()
    {
        var recipe = GetCuttingRecipeSOWithInput(KitchenObject.GetKitchenObjectSO());
        cuttingProgress++;

        OnAnyCut?.Invoke(this, EventArgs.Empty);

        if (cuttingProgress >= recipe.CuttingProgressMax)
        {
            var recipeOutput = recipe.Output;
            KitchenObject.DestroySelf();
            KitchenObject.SpawnKitchenObject(recipeOutput, this);
        }
        return (float)cuttingProgress / (float)recipe.CuttingProgressMax;
    }

    private bool HasRecipeWithInput(KitchenObjectSO input)
    {
        var recipe = GetCuttingRecipeSOWithInput(input);
        return recipe != null;
    }

    private bool HasRecipeWithOutput(KitchenObjectSO output)
    {
        var recipe = GetCuttingRecipeSOWithOutput(output);
        return recipe != null;
    }

    private CuttingRecipeSO GetCuttingRecipeSOWithInput(KitchenObjectSO input)
    {
        foreach (var recipe in cuttingRecipeSOArray)
        {
            if (recipe.Input == input)
                return recipe;
        }
        return null;
    }
    private CuttingRecipeSO GetCuttingRecipeSOWithOutput(KitchenObjectSO output)
    {
        foreach (var recipe in cuttingRecipeSOArray)
        {
            if (recipe.Output == output)
            {
                return recipe;
            }
        }
        return null;
    }
}
