using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class GameInput : MonoBehaviour {
    public static GameInput Instance  {private set;  get;}
    private PlayerInputConsole playerInputConsole;

    public event EventHandler OnInteractAction;
    public event EventHandler OnInteractAlternateAction;
    public event EventHandler OnPauseAction;

    private void Awake() {
        Instance = this;
        playerInputConsole = new PlayerInputConsole();
        playerInputConsole.Player.Enable();
        playerInputConsole.Player.Interact.performed += Interact_performed;
        playerInputConsole.Player.InteractAlternate.performed += InteractAlternate_performed;
        playerInputConsole.Player.Pause.performed += Pause_performed;
    }

    private void OnDestroy() {
        playerInputConsole.Player.Interact.performed -= Interact_performed;
        playerInputConsole.Player.InteractAlternate.performed -= InteractAlternate_performed;
        playerInputConsole.Player.Pause.performed -= Pause_performed;
        playerInputConsole.Dispose();
    }

    public Vector2 GetNormalisedMovementVector() {
        Vector2 inputVector = playerInputConsole.Player.Move.ReadValue<Vector2>();

        inputVector = inputVector.normalized;

        return inputVector;
    }

    private void Interact_performed(UnityEngine.InputSystem.InputAction.CallbackContext callbackContext) {
        OnInteractAction?.Invoke(this, EventArgs.Empty);
    }

    private void InteractAlternate_performed(UnityEngine.InputSystem.InputAction.CallbackContext callbackContext) {
        OnInteractAlternateAction?.Invoke(this, EventArgs.Empty);
    }

    private void Pause_performed(InputAction.CallbackContext context) {
        OnPauseAction?.Invoke(this, EventArgs.Empty);
    }
}
