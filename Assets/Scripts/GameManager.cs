using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {
    public static GameManager Instance  {private set;  get;}
    private enum State {
        WaitingToStart,
        CountdownToStart,
        GamePlayimg,
        GameOver
    }

    private State state;
    private float waitingToStartTimer = 1f;
    private float countdownToStartTimer = 3f;
    private float gamePlayimgTimer = 10f;
    
    private void Awake() {
        state = State.WaitingToStart;
        Instance = this;
    }

    private void Update() {
        switch(state) {
            case State.WaitingToStart:
                waitingToStartTimer -= Time.deltaTime;
                if (waitingToStartTimer < 0) {
                    state = State.CountdownToStart;
                }
                break;
            case State.CountdownToStart:
                countdownToStartTimer -= Time.deltaTime;
                if (countdownToStartTimer < 0) {
                    state = State.GamePlayimg;
                }
                break;
            case State.GamePlayimg:
                gamePlayimgTimer -= Time.deltaTime;
                if (gamePlayimgTimer < 0) {
                    state = State.GameOver;
                }
                break;
            case State.GameOver:
                break;
            
        }
    }

    public bool IsGamePLaying() {
        return state == State.GamePlayimg;
    }
}
