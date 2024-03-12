using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KitchenObject : MonoBehaviour {
    [SerializeField] private KitchenObjectSO kitchenObjectSO;

    private IKitchenObjectParent kitchenObjectParent;

    public IKitchenObjectParent GetKitchenObjectParent() {
        return kitchenObjectParent;
    }

    public void SetKitchenObjectParent(IKitchenObjectParent value) {
        if (value.HasKitchenObject()) {
            value.ClearKitchebObject();
        }

        if (kitchenObjectParent != null) {
            kitchenObjectParent.ClearKitchebObject();
        }

        kitchenObjectParent = value;

        if (kitchenObjectParent.HasKitchenObject()) {
            Debug.LogError("KitchenObjectParent is already has object");
        }
        kitchenObjectParent.SetKitchenObject(this);

        transform.SetParent(value.GetKitchenObjectFollowTransform());
        transform.localPosition = Vector3.zero;

    }

    public KitchenObjectSO GetKitchenObjectSO() {
        return kitchenObjectSO;
    }
}
