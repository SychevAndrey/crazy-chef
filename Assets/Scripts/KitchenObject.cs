using System.Collections;
using System.Collections.Generic;
using UnityEditor.PackageManager;
using UnityEngine;

public class KitchenObject : MonoBehaviour {
    [SerializeField] private KitchenObjectSO kitchenObjectSO;

    public static KitchenObject SpawnKitchenObject(KitchenObjectSO kitchenObjectSO, IKitchenObjectParent parent) {
        Transform kitchenObjectTransform = Instantiate(kitchenObjectSO.prefab);
        KitchenObject kitchenObject =  kitchenObjectTransform.GetComponent<KitchenObject>();
        kitchenObject.SetKitchenObjectParent(parent);

        return kitchenObject;
    }

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

    public void DestroySelf() {
        if (kitchenObjectParent != null) {
            kitchenObjectParent.ClearKitchebObject();
        }

        Destroy(gameObject);
    }
}

