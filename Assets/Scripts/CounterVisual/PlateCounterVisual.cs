using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlateCounterVisual : MonoBehaviour {
    [SerializeField] private PlateCounter plateCounter;
    [SerializeField] private Transform plateVisualPrefab;
    [SerializeField] private Transform counterTopPoint;

    private List<GameObject> spawnedPlateVisualsList;

    private void Awake() {
        spawnedPlateVisualsList = new List<GameObject>();
    }

    private void Start() {
        plateCounter.OnPlateSpawn += PlateCounter_OnPlateSpawn;
        plateCounter.OnPlatePickup += PlateCounter_OnPlatePickup;
    }

    private void PlateCounter_OnPlateSpawn(object sender, System.EventArgs e) {
        Transform gameVisualTransform = Instantiate(plateVisualPrefab, counterTopPoint);
        float plateOffsetY = 0.1f;
        gameVisualTransform.localPosition = new Vector3(0, spawnedPlateVisualsList.Count * plateOffsetY, 0);

        spawnedPlateVisualsList.Add(gameVisualTransform.gameObject);
    }

    private void PlateCounter_OnPlatePickup(object sender, System.EventArgs e) {
        GameObject platePicked = spawnedPlateVisualsList[spawnedPlateVisualsList.Count -1];
        spawnedPlateVisualsList.Remove(platePicked);
        Destroy(platePicked);
    }
}
