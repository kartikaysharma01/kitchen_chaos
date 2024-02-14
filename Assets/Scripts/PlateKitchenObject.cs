using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class  PlateKitchenObject : KitchenObject {

    public List<KitchenObjectSO> onPlateKitchenObjectSOList;
    public event EventHandler<OnIngredientAddArgs> OnIngredientAdd;
    public class OnIngredientAddArgs: EventArgs {
        public KitchenObjectSO kitchenObjectSO;
    }
    [SerializeField]  private List<KitchenObjectSO> validOnPlateKitchenObjectSOList;
    
    private void Awake() {
        onPlateKitchenObjectSOList = new List<KitchenObjectSO>();
    }
    public bool TryAddingToPlate(KitchenObjectSO kitchenObjectSO) {
        if (CanBePutOnPlate(kitchenObjectSO)) {
            // if object can be put on plate, add it to the list and fire an event to enable visual
            onPlateKitchenObjectSOList.Add(kitchenObjectSO);
            OnIngredientAdd?.Invoke(this, new OnIngredientAddArgs{
                kitchenObjectSO = kitchenObjectSO
            });
            return true;
        }
         return false;
    }

    private bool CanBePutOnPlate(KitchenObjectSO kitchenObjectSO) {
        if (ValidOnPlate(kitchenObjectSO)) {
            // ingredient is allowed put on plate
            if (!AlreadyOnPlate(kitchenObjectSO)) {
                // ingredient in not already on plate, can be put on plate
                return true;
            }
        }
        return false;
    }

    private bool ValidOnPlate(KitchenObjectSO kitchenObjectSO) {
        return validOnPlateKitchenObjectSOList.Contains(kitchenObjectSO);
    }

    private bool AlreadyOnPlate(KitchenObjectSO kitchenObjectSO) {
        return onPlateKitchenObjectSOList.Contains(kitchenObjectSO);
    }

    public List<KitchenObjectSO> GetonPlateKitchenObjectSOList() {
        return onPlateKitchenObjectSOList;
    }
}
