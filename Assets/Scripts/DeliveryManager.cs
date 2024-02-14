using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;


public class DeliveryManager : MonoBehaviour {
    public static DeliveryManager Instance {get; private set;}

    [SerializeField] private DishReceipeListSO dishReceipesListSO;

     private List<DishReceipeSO> waitingDishReceipeSOsList;
     private float spawnReceipeTimer;
     private float spawnReceipeTimerMax = 4f;
     private int waitingReceipeMax = 4;

    private void Awake() {
        Instance = this;
        waitingDishReceipeSOsList = new List<DishReceipeSO>();
    }

    private void Update() {
        spawnReceipeTimer -= Time.deltaTime;

        if (spawnReceipeTimer <= 0f) {
            // spawn a new reciepe (take a new order)
            if (waitingDishReceipeSOsList.Count < waitingReceipeMax) {
                spawnReceipeTimer = spawnReceipeTimerMax;

                DishReceipeSO dishReceipeSO = dishReceipesListSO.dishReceipeSOsList[Random.Range(0, dishReceipesListSO.dishReceipeSOsList.Count)];
                waitingDishReceipeSOsList.Add(dishReceipeSO);
                Debug.Log(dishReceipeSO);
            }
        }
    }

    public void DeliverDish(PlateKitchenObject plateKitchenObject) {
        for (int i=0; i< waitingDishReceipeSOsList.Count; i++) {
            if (plateKitchenObject.onPlateKitchenObjectSOList.Count == waitingDishReceipeSOsList[i].kitchenObjectSOsList.Count) {
                // plate has same number of ingridients as one of the waiting receipes
                VerifyPlateIngridientsWithReceipe(plateKitchenObject.onPlateKitchenObjectSOList, waitingDishReceipeSOsList[i].kitchenObjectSOsList);
            }
        }
    }

    private void VerifyPlateIngridientsWithReceipe(List<KitchenObjectSO> onPlateKitchenObjectSOList, List<KitchenObjectSO> waitingDishReceipeKitchenObjectSOsList) {
        // check if plate has any other ingridients than on waitingDishReceipeKitchenObjectSOsList
        bool correctDish = !onPlateKitchenObjectSOList.Except(waitingDishReceipeKitchenObjectSOsList).Any();
    }
}
