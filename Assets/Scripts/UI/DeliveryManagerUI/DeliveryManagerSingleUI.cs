using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DeliveryManagerSingleUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI receipeNameText;
    [SerializeField] private Transform iconContainer;
    [SerializeField] private Transform iconTemplate;

    private void Awake() {
        // hide template gameobject
        iconTemplate.gameObject.SetActive(false);
    }

    public void SetDishReceipeSO(DishReceipeSO dishReceipeSO) {
        receipeNameText.text = dishReceipeSO.receipeName;
        foreach(Transform child in iconContainer) {
            if (child == iconTemplate) continue;
            Destroy(child.gameObject);
        }
    
        foreach(KitchenObjectSO kitchenObjectSO in dishReceipeSO.kitchenObjectSOsList) {
            Transform receipeIngridientVisual = Instantiate(iconTemplate, iconContainer);
            receipeIngridientVisual.GetComponent<Image>().sprite = kitchenObjectSO.sprite;
            receipeIngridientVisual.gameObject.SetActive(true);
        }
    }
    
}
