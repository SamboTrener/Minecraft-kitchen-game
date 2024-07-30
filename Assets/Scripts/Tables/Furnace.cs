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
            roastingRecipeSO = GetRoastingRecipeSOWithInput(KitchenObject.GetKitchenObjectSO()); //������������ �����
        }
    }

    public override void Interact(Player player)
    {
        if (HasKitchenObject()) //� ���� ������
        {
            if (HasRecipeWithOutput(KitchenObject.GetKitchenObjectSO())) //������� 
            {
                if (!player.HasKitchenObject()) //� ������ � ����� �����
                {
                    KitchenObject.KitchenObjectParent = player;
                    OnFurnaceCleared?.Invoke(this, EventArgs.Empty); //������ ������ � ���� �����
                    HideBarEventInvoke();
                }
                else if (player.KitchenObject.TryGetPlate(out var plateKitchenObject)) //� ������ � ����� �������
                {
                    if (plateKitchenObject.TryAddIngredient(KitchenObject.GetKitchenObjectSO())) //� ������� ��� ��� ������ �����������
                    {
                        KitchenObject.DestroySelf();
                        OnFurnaceCleared?.Invoke(this, EventArgs.Empty); //������ ������ ����� �� �������
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
                OnFurnaceOccupied?.Invoke(this, EventArgs.Empty); //������ �������� �� ����
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
        if (roastingRecipeSO != null) // ���� ����� ��������� ���� ��� �������
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
