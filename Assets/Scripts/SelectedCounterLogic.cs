using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectedCounterLogic : MonoBehaviour {
    
    [SerializeField] private ClearCounter clearCounter;
    [SerializeField] private GameObject selctedVisualGameObject;

    private void Start(){
        Player.Instance.OnSeledtedCounterChange += Player_OnSeledtedCounterChange;
    }

    private void Player_OnSeledtedCounterChange(object sender, Player.OnSeledtedCounterChangeArgs e) {
        if (e.selectedCounter == clearCounter) {
            Show();
        } else {
            Hide();
        }
    }

    private void Show() {
        selctedVisualGameObject.SetActive(true);
    }

    private void Hide() {
        selctedVisualGameObject.SetActive(false);
    }
}
