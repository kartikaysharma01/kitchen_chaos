using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameInput : MonoBehaviour {
    private PlayerInputConsole playerInputConsole;

    public event EventHandler OnInteractAction;
    public event EventHandler OnInteractAlternateAction;

    private void Awake() {
        playerInputConsole = new PlayerInputConsole();
        playerInputConsole.Player.Enable();
        playerInputConsole.Player.Interact.performed += Interact_performed;
        playerInputConsole.Player.InteractAlternate.performed += InteractAlternate_performed;
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
}
