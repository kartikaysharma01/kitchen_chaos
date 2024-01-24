using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrashCounter : BaseCounter {

    // on interact, let player drop kitchen objects in trash
    public override void Interact(Player player) {
        if (player.HasKitchenObject()) {
            // if player is carrying a kitchen object, destroy it
            player.GetKitchenObject().DestroySelf();
        }
    }


}
