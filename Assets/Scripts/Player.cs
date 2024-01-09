using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {

    [SerializeField] private float moveSpeed;
    [SerializeField] private GameInput gameInput;
    private bool isWalking;
    private void Update() {
        Vector2 inputVector = gameInput.GetNormalisedMovementVector();

        Vector3 moveDir = new(inputVector.x, 0, inputVector.y);

        float playerHeight = 2f;
        float playerRadius = 0.7f;
        float moveDistance = moveSpeed * Time.deltaTime;
        bool canNotMove = Physics.CapsuleCast(transform.position, transform.position + Vector3.up * playerHeight, playerRadius, moveDir, moveDistance);

        if (canNotMove) {
            // can not move in moveDir

            // attempt moving in x direction
            Vector3 moveDirX = new(moveDir.x, 0, 0);
            bool canNotMoveX = Physics.CapsuleCast(transform.position, transform.position + Vector3.up * playerHeight, playerRadius, moveDirX, moveDistance);
            if (canNotMoveX) {
                // attempt moving in Z direction
                Vector3 moveDirZ = new(0, 0, moveDir.z);
                bool canNotMoveZ = Physics.CapsuleCast(transform.position, transform.position + Vector3.up * playerHeight, playerRadius, moveDirZ, moveDistance);
                if (canNotMoveZ) {
                    // can not move in any direction
                }
                else {
                    // move in Z
                    transform.position += moveDistance * moveDirZ.normalized;
                }
            }
            else {
                // move in X
                transform.position += moveDistance * moveDirX.normalized;
            }
        }
        else {
            transform.position += moveDistance * moveDir;
        }

        isWalking = moveDir != Vector3.zero;
         
        float rotateSpeed = 10f;
        transform.forward = Vector3.Slerp(transform.forward, moveDir, Time.deltaTime * rotateSpeed);
    }

    public bool IsWalking() {
        return isWalking;
    }
}
