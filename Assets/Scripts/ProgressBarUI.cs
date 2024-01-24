using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem.HID;
using UnityEngine.UI;

public class ProgressBarUI : MonoBehaviour {
    [SerializeField] private Image barImage;
    [SerializeField] private CuttingCounter cuttingCounter;

    private void Start() {
        cuttingCounter.OnCuttingProgressChange += CuttingCounter_OnCuttingProgressChange;

        barImage.fillAmount = 0f;
        Hide();
    }

    private void CuttingCounter_OnCuttingProgressChange(object sender, CuttingCounter.OnCuttingProgressChangeArgs e) {
        barImage.fillAmount = e.progressNormalised;

        if(e.progressNormalised == 0f || e.progressNormalised == 1f) {
            Hide();
        } else {
            Show();
        }
    }

    private void Show() {
        gameObject.SetActive(true);
    }

    private void Hide() {
        gameObject.SetActive(false);
    }
}
