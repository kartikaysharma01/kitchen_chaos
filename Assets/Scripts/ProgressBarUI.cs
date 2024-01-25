using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem.HID;
using UnityEngine.UI;

public class ProgressBarUI : MonoBehaviour {
    [SerializeField] private Image barImage;
    [SerializeField] private GameObject hasProgressBarGO;
    private IProgressBar progressBar;

    private void Start() {
        progressBar = hasProgressBarGO.GetComponent<IProgressBar>();
        if (progressBar == null) {
            Debug.LogError("GameObject goes not implement IProgressBar interface");
        }
        progressBar.OnProgressChange += ProgressBar_OnProgressChange;

        barImage.fillAmount = 0f;
        Hide();
    }

    private void ProgressBar_OnProgressChange(object sender, IProgressBar.OnProgressChangeArgs e) {
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
