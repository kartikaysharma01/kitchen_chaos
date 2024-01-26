using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContainerCounter : BaseCounter, IKitchenObjectParent {
    [SerializeField] private KitchenObjectSO kitchenObjectSO;

    public event EventHandler OnPlayerGrabbedObject;

    // on interact, play counter open_close animation, spawn a kitchen object and give it to the player
    public override void Interact(Player player) {
        if (!player.HasKitchenObject()) {
            // player is not carrying anything, let him pickup
            OnPlayerGrabbedObject?.Invoke(this, EventArgs.Empty);

            KitchenObject.SpawnKitchenObject(kitchenObjectSO, player);
        }
    }
}
