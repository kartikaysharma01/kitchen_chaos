using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// if player comes in interactable distance to a counter, shows the selected counter visual
public class SelectedCounterLogic : MonoBehaviour {
    
    [SerializeField] private BaseCounter baseCounter;
    [SerializeField] private GameObject[] selctedVisualGameObjectArray;

    private void Start(){
        Player.Instance.OnSeledtedCounterChange += Player_OnSeledtedCounterChange;
    }

    private void Player_OnSeledtedCounterChange(object sender, Player.OnSeledtedCounterChangeArgs e) {
        if (e.selectedCounter == baseCounter) {
            Show();
        } else {
            Hide();
        }
    }

    private void Show() {
        foreach (GameObject selctedVisualGameObject in selctedVisualGameObjectArray) {
            selctedVisualGameObject.SetActive(true);
        }
    }

    private void Hide() {
        foreach (GameObject selctedVisualGameObject in selctedVisualGameObjectArray) {
            selctedVisualGameObject.SetActive(false);
        }
    }
}
