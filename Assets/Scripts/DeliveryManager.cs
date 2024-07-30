using System;
using System.Collections.Generic;
using UnityEngine;

public class DeliveryManager : MonoBehaviour
{
    public event EventHandler OnRecipeSpawned;
    public event EventHandler OnRecipeCompleted;
    public event EventHandler OnRecipeSuccess;
    public event EventHandler OnRecipeFailed;


    public static DeliveryManager Instance { get; private set; }

    [SerializeField] RecipeListSO recipeListSO;
    List<RecipeSO> waitingRecipeSOList = new List<RecipeSO>();

    float spawnRecipeTimer;
    float spawnRecipeTimerMax = 6f;
    int waitingRecipesMax = 3;
    public int SuccessfulRecipesAmount { get; private set; } = 0;
    public int MoneyEarned { get; private set; } = 0;

    void Awake()
    {
        Instance = this;
    }

    void Update()
    {
        spawnRecipeTimer -= Time.deltaTime;
        if(spawnRecipeTimer <= 0f)
        {
            spawnRecipeTimer = spawnRecipeTimerMax;

            if(waitingRecipeSOList.Count <= waitingRecipesMax)
            {
                var waitingRecipeSO = recipeListSO.recipeSoList[UnityEngine.Random.Range(0, recipeListSO.recipeSoList.Count)];
                waitingRecipeSOList.Add(waitingRecipeSO);

                OnRecipeSpawned?.Invoke(this, EventArgs.Empty);
            }
        }
    }

    public void DeliverRecipe(PlateKitchenObject plate)
    {
        for(int i = 0; i < waitingRecipeSOList.Count; i++)
        {
            var waitingRecipeSO = waitingRecipeSOList[i];

            if(waitingRecipeSO.kitchenObjectSOList.Count == plate.GetKitchenObjectSOList().Count)
            {
                var plateContentMatchesRecipe = true;
                foreach (var recipeKitchenObjectSO in waitingRecipeSO.kitchenObjectSOList)
                {
                    var ingredientFound = false;
                    foreach(var plateKitchenObjectSO in plate.GetKitchenObjectSOList())
                    {
                        if(plateKitchenObjectSO == recipeKitchenObjectSO)
                        {
                            ingredientFound = true;
                            break;
                        }
                    }
                    if (!ingredientFound)
                    {
                        plateContentMatchesRecipe = false;
                    }
                }
                if (plateContentMatchesRecipe)
                {
                    SuccessfulRecipesAmount++;
                    MoneyEarned += waitingRecipeSOList[i].recipeCost;
                    waitingRecipeSOList.RemoveAt(i);
                    OnRecipeCompleted?.Invoke(this, EventArgs.Empty);
                    OnRecipeSuccess?.Invoke(this, EventArgs.Empty);
                    return;
                }
            }
        }
        OnRecipeFailed?.Invoke(this, EventArgs.Empty);
    }

    public List<RecipeSO> GetWaitingRecipeSOList() => waitingRecipeSOList;
}
