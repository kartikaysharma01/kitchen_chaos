using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameInput : MonoBehaviour {
    private PlayerInputConsole playerInputConsole;
    public event EventHandler OnInteractAction;

    private void Awake() {
        playerInputConsole = new PlayerInputConsole();
        playerInputConsole.Player.Enable();
        playerInputConsole.Player.Interact.performed += Interact_performed;
    }

    private void Interact_performed(UnityEngine.InputSystem.InputAction.CallbackContext callbackContext) {
        OnInteractAction?.Invoke(this, EventArgs.Empty);
    }

    public Vector2 GetNormalisedMovementVector() {
        Vector2 inputVector = playerInputConsole.Player.Move.ReadValue<Vector2>();

        inputVector = inputVector.normalized;

        return inputVector;
    }
}
