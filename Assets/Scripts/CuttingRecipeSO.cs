using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "CuttingRecipeSO", menuName = "ScriptableObjects/CuttingRecipeSO", order = 1)]
public class CuttingRecipeSO : ScriptableObject {
    public KitchenObjectSO input;
    public KitchenObjectSO output;
    public int cuttingProgressRequired;
}
