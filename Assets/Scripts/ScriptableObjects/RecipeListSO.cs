using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// [CreateAssetMenu(fileName = "RecipeListSO", menuName = "ScriptableObjects/RecipeListSO", order = 1)]
public class RecipeListSO : ScriptableObject
{
    public List<RecipeSO> recipes;

    public RecipeSO GetRandomRecipe()
    {
        return recipes[Random.Range(0, recipes.Count)];
    }
}
