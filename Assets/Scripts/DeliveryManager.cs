using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using UnityEngine.EventSystems;
using System;


public class DeliveryManager : MonoBehaviour {
    public static DeliveryManager Instance {get; private set;}

    [SerializeField] private DishReceipeListSO dishReceipesListSO;

    public event EventHandler OnReceipeSpawned;
    public event EventHandler OnDishDelivered;
    public event EventHandler OnSuccessfulDelivery;
    public event EventHandler OnFailedDelivery;

    private List<DishReceipeSO> waitingDishReceipeSOsList;
    private float spawnReceipeTimer;
    private float spawnReceipeTimerMax = 4f;
    private int waitingReceipeMax = 4;

    private int successfulDeliveryAmount;

    private void Awake() {
        Instance = this;
        waitingDishReceipeSOsList = new List<DishReceipeSO>();
    }

    private void Update() {
        spawnReceipeTimer -= Time.deltaTime;

        if (spawnReceipeTimer <= 0f) {
            // spawn a new reciepe (take a new order)
            if (GameManager.Instance.IsGamePLaying() && waitingDishReceipeSOsList.Count < waitingReceipeMax) {
                spawnReceipeTimer = spawnReceipeTimerMax;

                DishReceipeSO dishReceipeSO = dishReceipesListSO.dishReceipeSOsList[UnityEngine.Random.Range(0, dishReceipesListSO.dishReceipeSOsList.Count)];
                waitingDishReceipeSOsList.Add(dishReceipeSO);
                OnReceipeSpawned?.Invoke(this, EventArgs.Empty);
            }
        }
    }

    public void DeliverDish(PlateKitchenObject plateKitchenObject) {
        for (int i=0; i< waitingDishReceipeSOsList.Count; i++) {
            if (plateKitchenObject.onPlateKitchenObjectSOList.Count == waitingDishReceipeSOsList[i].kitchenObjectSOsList.Count) {
                // plate has same number of ingridients as one of the waiting receipes
                bool correctDish = VerifyPlateIngridientsWithReceipe(plateKitchenObject.onPlateKitchenObjectSOList, waitingDishReceipeSOsList[i].kitchenObjectSOsList);
                if (correctDish) {
                    waitingDishReceipeSOsList.RemoveAt(i);
                    OnDishDelivered?.Invoke(this, EventArgs.Empty);
                    OnSuccessfulDelivery?.Invoke(this, EventArgs.Empty);
                    successfulDeliveryAmount++;
                    return;
                }
            }
        }
        OnFailedDelivery?.Invoke(this, EventArgs.Empty);
    }

    private bool VerifyPlateIngridientsWithReceipe(List<KitchenObjectSO> onPlateKitchenObjectSOList, List<KitchenObjectSO> waitingDishReceipeKitchenObjectSOsList) {
        // check if plate has any other ingridients than on waitingDishReceipeKitchenObjectSOsList
        return !onPlateKitchenObjectSOList.Except(waitingDishReceipeKitchenObjectSOsList).Any();
    }

    public List<DishReceipeSO> getWaitingDishReceipeSOsList() {
        return waitingDishReceipeSOsList;
    }

    public int GetSuccessfulDeliveryAmount() {
        return successfulDeliveryAmount;
    }
}
