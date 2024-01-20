using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CuttingCounter : BaseCounter {
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
                // player is carrying something, do nothing
            } else {
                // player is not carrying anything, let him pickup counter object
                GetKitchenObject().SetKitchenObjectParent(player);
            }
        }
    }
 
}
