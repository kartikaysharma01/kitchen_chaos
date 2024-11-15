using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {
    public static GameManager Instance  {private set;  get;}

    public event EventHandler OnStateChange;
    public event EventHandler OnGamePaused;
    public event EventHandler OnGameUnPaused;

    private enum State {
        WaitingToStart,
        CountdownToStart,
        GamePlayimg,
        GameOver
    }

    private State state;
    private float countdownToStartTimer = 3f;
    private float gamePlayimgTimer;
    private float gamePlayimgTimerMax = 20f;
    private bool isGamePaused;
    
    private void Awake() {
        state = State.WaitingToStart;
        Instance = this;
    }

    private void Start() {
        GameInput.Instance.OnPauseAction += OnPauseAction_Listner;
        GameInput.Instance.OnInteractAction += GameInput_OnInteractAction;
    }

    private void GameInput_OnInteractAction(object sender, EventArgs e){
        if (state == State.WaitingToStart) {
            state = State.CountdownToStart;
            OnStateChange?.Invoke(this, EventArgs.Empty);
        }
    }

    private void OnPauseAction_Listner(object sender, EventArgs e) {
        TogglePause();
    }

    private void Update() {
        switch(state) {
            case State.WaitingToStart:
                break;
            case State.CountdownToStart:
                countdownToStartTimer -= Time.deltaTime;
                if (countdownToStartTimer < 0) {
                    state = State.GamePlayimg;
                    gamePlayimgTimer = gamePlayimgTimerMax;
                    OnStateChange?.Invoke(this, EventArgs.Empty);
                }
                break;
            case State.GamePlayimg:
                gamePlayimgTimer -= Time.deltaTime;
                if (gamePlayimgTimer < 0) {
                    state = State.GameOver;
                    OnStateChange?.Invoke(this, EventArgs.Empty);
                }
                break;
            case State.GameOver:
                break;
            
        }
    }

    public bool IsGamePLaying() {
        return state == State.GamePlayimg;
    }

    public bool InCountDownState() {
        return state == State.CountdownToStart;
    }

    public float GetCountdownToStartTimer() {
        return countdownToStartTimer;
    }

    public bool IsGameOver() {
        return state == State.GameOver;
    }

    public float GetGamePlayingTimerNormalised() {
        return (gamePlayimgTimer/gamePlayimgTimerMax);
    }

    public void TogglePause() {
        isGamePaused = !isGamePaused;
        if (isGamePaused) {
            Time.timeScale = 0f;
            OnGamePaused?.Invoke(this, EventArgs.Empty);
        } else {
            Time.timeScale = 1f;
            OnGameUnPaused?.Invoke(this, EventArgs.Empty);
        }
    }
}
