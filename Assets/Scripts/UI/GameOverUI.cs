using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameOverUI : MonoBehaviour {
    [SerializeField] private TextMeshProUGUI deliveriesMadeText;

    private void Start() {
        GameManager.Instance.OnStateChange += GameManager_OnStateChange;
        Hide();
    }

    private void Update() {
        deliveriesMadeText.text = DeliveryManager.Instance.GetSuccessfulDeliveryAmount().ToString();
    }

    private void GameManager_OnStateChange(object sender, EventArgs e){
        if (GameManager.Instance.IsGameOver()) {
            Show();
        }
        else {
            Hide();
        }
    }

    private void Show() {
        gameObject.SetActive(true);
    }

    private void Hide() {
        gameObject.SetActive(false);
    }
}
