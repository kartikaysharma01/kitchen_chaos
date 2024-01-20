using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClearCounter : BaseCounter, IKitchenObjectParent {

    [SerializeField] private KitchenObjectSO kitchenObjectSO;

    // on interact, let player pickup or drop kitchen objects
    public override void Interact(Player player) {
    
    }

}
