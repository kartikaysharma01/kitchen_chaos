using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// base class for all counter types, provides base functions for counter <> kitchen object inteactions
public class BaseCounter : MonoBehaviour, IKitchenObjectParent {
    [SerializeField] private Transform counterTopPoint;
    
    private KitchenObject kitchenObject;

    public static event EventHandler OnObjectDropOnCounter;
    public static void ResetStaticData() {
        OnObjectDropOnCounter = null;
    }

    public virtual void Interact(Player player) {
        Debug.LogError("BaseCounter.Interact() was called. Specific Counter classes are supposed to override this call");
    }

    public virtual void InteractAlternate(Player player) {
        // to be overriden by some counters
    }
    
    public Transform GetKitechObjectSpawnPoint() {
        return counterTopPoint;
    }

    public void SetKitchenObject(KitchenObject kitchenObject) {
        this.kitchenObject = kitchenObject;

        if (kitchenObject != null) {
            OnObjectDropOnCounter?.Invoke(this, EventArgs.Empty);
        }
    }

    public KitchenObject GetKitchenObject() {
        return kitchenObject;
    }

    public void ClearKitchenObject() {
        kitchenObject = null;
    }

    public bool HasKitchenObject() {
        return kitchenObject != null;
    }
}
