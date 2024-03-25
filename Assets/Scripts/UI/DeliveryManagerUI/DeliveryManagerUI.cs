using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeliveryManagerUI : MonoBehaviour{

    [SerializeField] private Transform deliverySpawnContainer;
    [SerializeField] private Transform deliveryReceipeTemplate;
    
    private void Awake() {
        // hide template gameobject
        deliveryReceipeTemplate.gameObject.SetActive(false);
    }

    private void Start() {
        DeliveryManager.Instance.OnReceipeSpawned += DeliveryManager_OnReceipeSpawned;
        DeliveryManager.Instance.OnDishDelivered += DeliveryManager_OnDishDelivered;
    }

    private void DeliveryManager_OnReceipeSpawned(object sender, System.EventArgs e) {
        UpdateDeliverySpawnContainerVisual();
    }

    private void DeliveryManager_OnDishDelivered (object sender, System.EventArgs e) {
        UpdateDeliverySpawnContainerVisual();
    }

    private void UpdateDeliverySpawnContainerVisual() {
        foreach(Transform child in deliverySpawnContainer) {
            if (child == deliveryReceipeTemplate) continue;
            Destroy(child.gameObject);
        }
    
        foreach(DishReceipeSO waitingDishReceipe in DeliveryManager.Instance.getWaitingDishReceipeSOsList()) {
            Transform waitingDishReceipeVisual = Instantiate(deliveryReceipeTemplate, deliverySpawnContainer);
            waitingDishReceipeVisual.GetComponent<DeliveryManagerSingleUI>().SetDishReceipeSO(waitingDishReceipe);
            waitingDishReceipeVisual.gameObject.SetActive(true);
        }
    }
}
