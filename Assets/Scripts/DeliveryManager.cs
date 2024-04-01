using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.FullSerializer;
using UnityEditor.PackageManager;
using UnityEngine;

public class DeliveryManager : MonoBehaviour
{
    public static DeliveryManager Instance { get; private set; }
    [SerializeField] RecipeListSO recipeList;
    private List<RecipeSO> waitingRecipes;
    private float spawnRecipeTimer;
    private float spawnRecipeTimerMax = 5f;
    private int waitingRecipesMax = 5;
    private void Awake()
    {
        Instance = this;
        waitingRecipes = new List<RecipeSO>();
    }

    private void Update()
    {
        spawnRecipeTimer -= Time.deltaTime;
        if (spawnRecipeTimer <= 0 && waitingRecipes.Count < waitingRecipesMax)
        {
            spawnRecipeTimer = spawnRecipeTimerMax;
            SpawnRecipe();
        }
    }

    private void SpawnRecipe()
    {
        RecipeSO recipe = recipeList.GetRandomRecipe();
        waitingRecipes.Add(recipe);
    }

    public void DeliverRecipe(PlateKitchenObject plateKitchenObject)
    {
        foreach (RecipeSO recipe in waitingRecipes)
        {
            if (plateKitchenObject.CompareRecipe(recipe))
            {
                waitingRecipes.Remove(recipe);
                return;
            }
        }
    }
}
