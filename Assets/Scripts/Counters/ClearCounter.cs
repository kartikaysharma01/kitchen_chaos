using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClearCounter : BaseCounter, IKitchenObjectParent {

    [SerializeField] private KitchenObjectSO kitchenObjectSO;

    // on interact, let player pickup or drop kitchen objects
    public override void Interact(Player player) {
        if (!HasKitchenObject()) {
            // counter does not have kitchen object
            if (player.HasKitchenObject()) {
                // player is carrying something, let him drop it
                player.GetKitchenObject().SetKitchenObjectParent(this);
            } else {
                // player is not carrying anything, do nothing
            }
        } else {
            // counter has kitchen object
            if (player.HasKitchenObject()) {
                // player is carrying something
                if (player.GetKitchenObject().TryGetPlate(out PlateKitchenObject plateKitchenObject)) {
                    // player is carrying a plate, let him put counter object on the plate
                    if (plateKitchenObject.TryAddingToPlate(GetKitchenObject().GetKitchenObjectSO())){
                        // counter object is added to plate, destroy it
                        GetKitchenObject().DestroySelf();
                    }
                }
                else if (GetKitchenObject().TryGetPlate(out plateKitchenObject)) {
                    // player is not carrying plate, but counter object is a plate, try putting what he is carrying on plate
                    if (plateKitchenObject.TryAddingToPlate(player.GetKitchenObject().GetKitchenObjectSO())){
                        // player object is added to plate, destroy it
                        player.GetKitchenObject().DestroySelf();
                    }
                }
                else {
                    // plater is neither carrying a plate, nor is counter object a plate, do nothing
                }
            } else {
                // player is not carrying anything, let him pickup counter object
                GetKitchenObject().SetKitchenObjectParent(player);
            }
        }
    
    }

}
