using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DeliveryManagerSingleUI : MonoBehaviour
{
    [SerializeField] private Transform ingredientContainer;
    [SerializeField] private Transform iconTemplate;
    [SerializeField] private TextMeshProUGUI recipeNameText;

    private void Awake()
    {
        iconTemplate.gameObject.SetActive(false);
    }

    public void SetRecipe(RecipeSO recipe)
    {
        recipeNameText.text = recipe.recipeName;
        UpdateVisuals(recipe);
    }

    private void UpdateVisuals(RecipeSO recipe)
    {
        foreach (Transform child in ingredientContainer)
        {
            if (child == iconTemplate) continue;
            Destroy(child.gameObject);
        }

        foreach (KitchenObjectSO ingredient in recipe.ingredients)
        {
            Transform iconTransform = Instantiate(iconTemplate, ingredientContainer);
            iconTransform.gameObject.SetActive(true);
            iconTransform.GetComponent<Image>().sprite = ingredient.sprite;
        }
    }
}
