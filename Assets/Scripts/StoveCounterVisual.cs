using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoveCounterVisual : MonoBehaviour {

    [SerializeField] private StoveCounter stoveCounter;
    [SerializeField] private GameObject stoveOnVisualGO;
    [SerializeField] private GameObject sizzlingParticlesGO;

    private void Start() {
        stoveCounter.OnStateChnage += StoveCounter_OnStateChnage;
    }

    private void StoveCounter_OnStateChnage(object sender, StoveCounter.OnStateChnageArgs e) {
        bool showVisuals = e.state == StoveCounter.State.Frying || e.state == StoveCounter.State.Fried;
        stoveOnVisualGO.SetActive(showVisuals);
        sizzlingParticlesGO.SetActive(showVisuals);
    }
}
