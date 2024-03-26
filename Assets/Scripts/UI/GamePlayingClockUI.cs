using System.Collections;
using System.Collections.Generic;
using Microsoft.Unity.VisualStudio.Editor;
using UnityEngine;

public class GamePlayingClockUI : MonoBehaviour {
    [SerializeField] private UnityEngine.UI.Image timerImage;
    
    private void Update() {
        timerImage.fillAmount = GameManager.Instance.GetGamePlayingTimerNormalised();
    }
}
