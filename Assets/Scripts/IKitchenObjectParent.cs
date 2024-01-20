using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// interface to be adopted by player and counter to own kitchen objects
public interface IKitchenObjectParent{
    public Transform GetKitechObjectSpawnPoint();

    public void SetKitchenObject(KitchenObject kitchenObject);

    public KitchenObject GetKitchenObject();

    public void ClearKitchenObject();

    public bool HasKitchenObject();
}