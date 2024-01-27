using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Timeline;

public class PlateIconUI : MonoBehaviour
{
    [SerializeField] private PlateKitchenObject plateKitchenObject;
    [SerializeField] private Transform iconTemplate;

   private void Start() {
        plateKitchenObject.OnIngredientAdd += PlateKitchenObject_OnIngredientAdd;
    }

    private void PlateKitchenObject_OnIngredientAdd(object sender, PlateKitchenObject.OnIngredientAddArgs e) {
        UpdateVisual();
    }

    private void UpdateVisual() {
        foreach(Transform child in transform) {
            if (child == iconTemplate) continue;
            Destroy(child.gameObject);  
        }

        foreach(KitchenObjectSO kitchenObjectSO in plateKitchenObject.GetonPlateKitchenObjectSOList()) {
            Transform iconTemplateTransform = Instantiate(iconTemplate, transform);
            iconTemplateTransform.GetComponent<IconTemplateUI>().SetKitchenObjectSO(kitchenObjectSO);
            iconTemplateTransform.gameObject.SetActive(true);
        }
    }
}
