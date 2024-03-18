using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class CuttingCounter : BaseCounter {
    public event EventHandler<OnCuttingProgressChangedEventArgs> OnCuttingProgressChanged;
    public event EventHandler OnCutting;
    public class OnCuttingProgressChangedEventArgs : EventArgs {
        public float cuttingProgressNormalized;
    }

    [SerializeField] private CuttingRecipeSO[] cuttingRecipeSOArray;
    private int cuttingProgress = 0;
    public override void Interact(Player player) {
        if (!HasKitchenObject()) {
            cuttingProgress = 0;
            if (player.HasKitchenObject() && HasRecipeWithInput(player.GetKitchenObject().GetKitchenObjectSO())) {
                player.GetKitchenObject().SetKitchenObjectParent(this);
                int requiredCuttingProgress = GetCuttingRecipeSO(GetKitchenObject().GetKitchenObjectSO()).cuttingProgressRequired;
                OnCuttingProgressChanged?.Invoke(this, new OnCuttingProgressChangedEventArgs { cuttingProgressNormalized = (float)cuttingProgress / requiredCuttingProgress });
            }
        } else if (!player.HasKitchenObject()) {
            GetKitchenObject().SetKitchenObjectParent(player);
            cuttingProgress = 0;

            OnCuttingProgressChanged?.Invoke(this, new OnCuttingProgressChangedEventArgs { cuttingProgressNormalized = 0 });
        }
    }

    public override void InteractAlternate(Player player) {
        if (HasKitchenObject() && HasRecipeWithInput(GetKitchenObject().GetKitchenObjectSO())) {
            cuttingProgress++;

            int requiredCuttingProgress = GetCuttingRecipeSO(GetKitchenObject().GetKitchenObjectSO()).cuttingProgressRequired;
            OnCuttingProgressChanged?.Invoke(this, new OnCuttingProgressChangedEventArgs { cuttingProgressNormalized = (float)cuttingProgress / requiredCuttingProgress });

            if (cuttingProgress >0) {
                   OnCutting?.Invoke(this, EventArgs.Empty);
            }

            if (cuttingProgress >= requiredCuttingProgress) {
                KitchenObjectSO outputKitchenObjectSO = GetOutputKitchenObjectSO(GetKitchenObject().GetKitchenObjectSO());
                GetKitchenObject().DestroySelf();

                KitchenObject.SpawnKitchenObject(outputKitchenObjectSO, this);
            }
        }
    }

    private bool HasRecipeWithInput(KitchenObjectSO inputKitchenObjectSO) {
        return GetCuttingRecipeSO(inputKitchenObjectSO)?.input == inputKitchenObjectSO;
    }

    private KitchenObjectSO GetOutputKitchenObjectSO(KitchenObjectSO inputKitchenObjectSO) {
        return GetCuttingRecipeSO(inputKitchenObjectSO)?.output;
    }

    private CuttingRecipeSO GetCuttingRecipeSO(KitchenObjectSO inputKitchenObjectSO) {
        foreach (CuttingRecipeSO cuttingRecipeSO in cuttingRecipeSOArray) {
            if (cuttingRecipeSO.input == inputKitchenObjectSO) {
                return cuttingRecipeSO;
            }
        }

        return null;
    }
}
