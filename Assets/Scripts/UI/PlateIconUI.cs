using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlateIconUI : MonoBehaviour
{
    [SerializeField] private PlateKitchenObject plateKitchenObject;
    [SerializeField] private Transform iconTemplate;

    private void Awake()
    {
        iconTemplate.gameObject.SetActive(false);
    }

    void LateUpdate()
    {
        transform.forward = Camera.main.transform.forward;
    }

    private void Start()
    {
        plateKitchenObject.OnIngredientAdded += UpdatePlateIcon;
    }

    private void UpdatePlateIcon(object sender, PlateKitchenObject.OnIngredientAddedEventArgs e)
    {
        ClearVisuals();
        UpdateVisuals();
    }

    private void ClearVisuals()
    {
        foreach (Transform child in transform)
        {
            if (child == iconTemplate) continue;
            Destroy(child.gameObject);
        }
    }

    private void UpdateVisuals()
    {
        foreach (KitchenObjectSO kitchenObjectSO in plateKitchenObject.GetKitchenObjectSOList())
        {
            Transform iconTransform = Instantiate(iconTemplate, transform);
            iconTransform.gameObject.SetActive(true);
            iconTransform.GetComponent<PlateIconSingleUI>().SetKitchenObjectSO(kitchenObjectSO);
        }
    }
}
