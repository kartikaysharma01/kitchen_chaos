using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CuttingCounter : BaseCounter {
    [SerializeField] private CuttingReceipeSO[] cuttingReceipeSOArray;

    // on interact, let player pickup or drop kitchen objects
    public override void Interact(Player player) {
        if (!HasKitchenObject()) {
            // counter does not have kitchen object
            if (player.HasKitchenObject()) {
                // player is carrying something
                if (HasReceipeWithInput(player.GetKitchenObject().GetKitchenObjectSO())) {
                    // if what player is carrying can be cut, let him drop it
                    player.GetKitchenObject().SetKitchenObjectParent(this);
                }
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

    // if kitchen object is found, destroys it and spawns a cut one
    public override void InteractAlternate(Player player) {
        if (HasKitchenObject() && HasReceipeWithInput(GetKitchenObject().GetKitchenObjectSO())) {
            // There is a kitchen object here which can be cut, chop chop
            KitchenObjectSO outputKitchenObject = GetOutputForInput(GetKitchenObject().GetKitchenObjectSO());
            GetKitchenObject().DestroySelf();

            KitchenObject.SpawnKitchenObject(outputKitchenObject, this);
        }
    }

    private bool HasReceipeWithInput(KitchenObjectSO kitchenObjectSO) {
        foreach(CuttingReceipeSO cuttingReceipeSO in cuttingReceipeSOArray) {
            if (cuttingReceipeSO.input == kitchenObjectSO) {
                return true;
            }
        }
        return false;
    }

    private KitchenObjectSO GetOutputForInput(KitchenObjectSO inputKitchenObjectSO) {
        foreach(CuttingReceipeSO cuttingReceipeSO in cuttingReceipeSOArray) {
            if (cuttingReceipeSO.input == inputKitchenObjectSO) {
                return cuttingReceipeSO.output;
            }
        }
        return null;
    }
}
