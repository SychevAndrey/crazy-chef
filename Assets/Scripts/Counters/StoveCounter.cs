using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoveCounter : BaseCounter, IProgressBar
{
    [SerializeField] private FryingRecipeSO[] fryingRecipeSOArray;
    private float fryingTimer;
    private FryingRecipeSO fryingRecipeSO;
    private State state = State.Empty;
    public event EventHandler<onStateChangedEventArgs> OnStateChanged;
    public event EventHandler<IProgressBar.OnProgressChangedEventArgs> OnProgressChanged;
    public class onStateChangedEventArgs : EventArgs
    {
        public State state;
    }
    public enum State
    {
        Empty,
        Cooking,
        Cooked,
        Burnt
    }

    private void Update()
    {
        if (HasKitchenObject())
        {
            switch (state)
            {
                case State.Empty:
                    break;
                case State.Cooking:
                    CookingState();
                    break;
                case State.Cooked:
                    CookedState();
                    break;
                case State.Burnt:
                    BurntState();
                    break;
            }
        }
    }

    private void BurntState()
    {
        OnProgressChanged.Invoke(this, new IProgressBar.OnProgressChangedEventArgs { progressNormalized = 0f });
    }

    private void CookedState()
    {
        fryingTimer += Time.deltaTime;
        OnProgressChanged?.Invoke(this, new IProgressBar.OnProgressChangedEventArgs { progressNormalized = (float)fryingTimer / fryingRecipeSO.fryingTimerMax });
        if (fryingTimer >= fryingRecipeSO.fryingTimerMax)
        {
            GetKitchenObject().DestroySelf();
            KitchenObject.SpawnKitchenObject(GetOutputKitchenObjectSO(fryingRecipeSO.input), this);
            state = State.Burnt;

            OnStateChanged?.Invoke(this, new onStateChangedEventArgs { state = state });
        }
    }

    private void CookingState()
    {
        fryingTimer += Time.deltaTime;

        OnProgressChanged?.Invoke(this, new IProgressBar.OnProgressChangedEventArgs { progressNormalized = (float)fryingTimer / fryingRecipeSO.fryingTimerMax });
        if (fryingTimer >= fryingRecipeSO.fryingTimerMax)
        {
            GetKitchenObject().DestroySelf();
            KitchenObject.SpawnKitchenObject(GetOutputKitchenObjectSO(fryingRecipeSO.input), this);
            fryingRecipeSO = GetFryingRecipeSO(GetKitchenObject().GetKitchenObjectSO());
            fryingTimer = 0f;
            state = State.Cooked;

            OnStateChanged?.Invoke(this, new onStateChangedEventArgs { state = state });
        }
    }

    public override void Interact(Player player)
    {
        if (!HasKitchenObject())
        {
            if (player.HasKitchenObject() && HasRecipeWithInput(player.GetKitchenObject().GetKitchenObjectSO()))
            {
                player.GetKitchenObject().SetKitchenObjectParent(this);
                fryingRecipeSO = GetFryingRecipeSO(GetKitchenObject().GetKitchenObjectSO());
                state = State.Cooking;
                fryingTimer = 0f;
                OnStateChanged?.Invoke(this, new onStateChangedEventArgs { state = state });
            }
        }
        else if (!player.HasKitchenObject())
        {
            GetKitchenObject().SetKitchenObjectParent(player);
            ResetState();
        }
        else if (player.HasKitchenObject())
        {
            if (player.GetKitchenObject().TryGetPlate(out PlateKitchenObject plateKitchenObject))
            {
                if (plateKitchenObject.TryAddIngredient(GetKitchenObject().GetKitchenObjectSO()))
                {
                    GetKitchenObject().DestroySelf();
                    ResetState();
                }
            }
        }
    }

    private void ResetState()
    {
        state = State.Empty;
        fryingTimer = 0f;
        fryingRecipeSO = null;
        OnStateChanged?.Invoke(this, new onStateChangedEventArgs { state = state });
        OnProgressChanged.Invoke(this, new IProgressBar.OnProgressChangedEventArgs { progressNormalized = 0f });
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
