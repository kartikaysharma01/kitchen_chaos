using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class  PlateKitchenObject : KitchenObject {

    private List<KitchenObjectSO> onPlatekitchenObjectSOList;
    [SerializeField]  private List<KitchenObjectSO> validOnPlateKitchenObjectSOList;
    
    private void Awake() {
        onPlatekitchenObjectSOList = new List<KitchenObjectSO>();
    }
    public bool TryAddingToPlate(KitchenObjectSO kitchenObjectSO) {
        if (CanBePutOnPlate(kitchenObjectSO)) {
            onPlatekitchenObjectSOList.Add(kitchenObjectSO);
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
        return onPlatekitchenObjectSOList.Contains(kitchenObjectSO);
    }

}
