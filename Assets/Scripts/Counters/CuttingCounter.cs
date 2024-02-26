using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;

public class CuttingCounter : BaseCounter, IProgressBar {
    [SerializeField] private CuttingReceipeSO[] cuttingReceipeSOArray;
    private int cuttingProgress;

    public event EventHandler OnCut;
    public static event EventHandler OnAnyCut;
    public event EventHandler<IProgressBar.OnProgressChangeArgs> OnProgressChange;

    // on interact, let player pickup or drop kitchen objects
    public override void Interact(Player player) {
        if (!HasKitchenObject()) {
            // counter does not have kitchen object
            if (player.HasKitchenObject()) {
                // player is carrying something
                if (HasReceipeWithInput(player.GetKitchenObject().GetKitchenObjectSO())) {
                    // if what player is carrying can be cut, let him drop it
                    player.GetKitchenObject().SetKitchenObjectParent(this);
                    cuttingProgress = 0;
                    OnProgressChange?.Invoke(this, new IProgressBar.OnProgressChangeArgs{
                        progressNormalised = cuttingProgress
                    });
                }
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
            } else {
                // player is not carrying anything, let him pickup counter object
                GetKitchenObject().SetKitchenObjectParent(player);
            }
        }
    }

    // if kitchen object is found, destroys it and spawns a cut one
    public override void InteractAlternate(Player player) {
        if (HasKitchenObject() && HasReceipeWithInput(GetKitchenObject().GetKitchenObjectSO())) {
            cuttingProgress++; 
            CuttingReceipeSO cuttingReceipeSO = GetCuttingReceipeSOWithInput(GetKitchenObject().GetKitchenObjectSO());
            OnCut?.Invoke(this, EventArgs.Empty);
            OnAnyCut?.Invoke(this, EventArgs.Empty);
            OnProgressChange?.Invoke(this, new IProgressBar.OnProgressChangeArgs{
                progressNormalised = (float)cuttingProgress / cuttingReceipeSO.cuttingProgressMax
            });

            // There is a kitchen object here which can be cut, chop chop
            if (cuttingProgress >= cuttingReceipeSO.cuttingProgressMax) {
                KitchenObjectSO outputKitchenObject = GetOutputForInput(GetKitchenObject().GetKitchenObjectSO());
                GetKitchenObject().DestroySelf();

                KitchenObject.SpawnKitchenObject(cuttingReceipeSO.output, this);
            }
        }
    }

    private bool HasReceipeWithInput(KitchenObjectSO kitchenObjectSO) {
        CuttingReceipeSO cuttingReceipeSO = GetCuttingReceipeSOWithInput(kitchenObjectSO);
        if (cuttingReceipeSO != null) {
            return true;
        }
        return false;
    }

    private KitchenObjectSO GetOutputForInput(KitchenObjectSO inputKitchenObjectSO) {
        CuttingReceipeSO cuttingReceipeSO = GetCuttingReceipeSOWithInput(inputKitchenObjectSO);
        if (cuttingReceipeSO != null) {
            return cuttingReceipeSO.output;
        }
        return null;
    }

    private CuttingReceipeSO GetCuttingReceipeSOWithInput(KitchenObjectSO inputKitchenObjectSO) {
        foreach(CuttingReceipeSO cuttingReceipeSO in cuttingReceipeSOArray) {
            if (cuttingReceipeSO.input == inputKitchenObjectSO) {
                return cuttingReceipeSO;
            }
        }
        return null;
    }
}
