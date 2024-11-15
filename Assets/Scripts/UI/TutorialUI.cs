using System;
using UnityEngine;
public class TutorialUI : MonoBehaviour
{
    private void Start() {
        GameManager.Instance.OnStateChange += GameManager_OnStateChange;
        Show();
    }

    private void GameManager_OnStateChange(object sender, EventArgs e)
    {
        if(GameManager.Instance.InCountDownState()) {
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
