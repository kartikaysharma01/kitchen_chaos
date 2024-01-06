using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameInput : MonoBehaviour {
    private PlayerInputConsole playerInputConsole;

    private void Awake() {
        playerInputConsole = new PlayerInputConsole();
        playerInputConsole.Player.Enable();
    }

    public Vector2 GetNormalisedMovementVector() {
        Vector2 inputVector = playerInputConsole.Player.Move.ReadValue<Vector2>();

        inputVector = inputVector.normalized;

        return inputVector;
    }
}
