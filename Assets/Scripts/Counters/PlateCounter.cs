using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlateCounter : BaseCounter {
  
    private float plateSpawnTimer;
    private readonly float plateSpawnTimeLimit = 4f;
    private int platesSpawned;
    private readonly int platesSpawnMax = 4;

    public event EventHandler OnPlateSpawn;
    public event EventHandler OnPlatePickup;
    [SerializeField] private KitchenObjectSO plateKitchenObjectSO;

    private void Update() {
        plateSpawnTimer += Time.deltaTime;

        if (plateSpawnTimer > plateSpawnTimeLimit && platesSpawned < platesSpawnMax) {
            // fire an event to spawn a new plate visual, reset spawn timer
            plateSpawnTimer = 0;
            platesSpawned++;
            OnPlateSpawn?.Invoke(this, EventArgs.Empty);
        }
    }
    // on interact, hand player a plate if avaialble
    public override void Interact(Player player) {
         if (!player.HasKitchenObject() && platesSpawned > 0) {
            // player is not carrying anything and a plate is available, let him pickup plate
            plateSpawnTimer = 0;
            platesSpawned--;
            OnPlatePickup?.Invoke(this, EventArgs.Empty);

            KitchenObject.SpawnKitchenObject(plateKitchenObjectSO, player);
        }
    }

}
