using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoveCounter : BaseCounter
{
    [SerializeField] private FryingRecipeSO[] fryingRecipeSOArray;

    public override void Interact(Player player)
    {
        if (!HasKitchenObject())
        {
            if (player.HasKitchenObject() && HasRecipeWithInput(player.GetKitchenObject().GetKitchenObjectSO()))
            {
                player.GetKitchenObject().SetKitchenObjectParent(this);
            }
        }
        else if (!player.HasKitchenObject())
        {
            GetKitchenObject().SetKitchenObjectParent(player);
        }
    }

    private bool HasRecipeWithInput(KitchenObjectSO inputKitchenObjectSO)
    {
        return GetFryingRecipeSO(inputKitchenObjectSO)?.input == inputKitchenObjectSO;
    }

    private KitchenObjectSO GetOutputKitchenObjectSO(KitchenObjectSO inputKitchenObjectSO)
    {
        return GetFryingRecipeSO(inputKitchenObjectSO)?.output;
    }

    private FryingRecipeSO GetFryingRecipeSO(KitchenObjectSO inputKitchenObjectSO)
    {
        foreach (FryingRecipeSO fryingRecipeSO in fryingRecipeSOArray)
        {
            if (fryingRecipeSO.input == inputKitchenObjectSO)
            {
                return fryingRecipeSO;
            }
        }

        return null;
    }
}
