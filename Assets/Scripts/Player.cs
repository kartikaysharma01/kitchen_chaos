using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {

    [SerializeField] private float moveSpeed = 7f;
    private bool isWalking;
    private void Update() {
        Vector2 inputVector = new(0,0);

        if (Input.GetKey(KeyCode.W)) {
            inputVector.y += 1;
        }
        if (Input.GetKey(KeyCode.A)) {
            inputVector.x += -1;
        }
        if (Input.GetKey(KeyCode.S)) {
            inputVector.y += -1;
        }
        if (Input.GetKey(KeyCode.D)) {
            inputVector.x += 1;
        }

        inputVector = inputVector.normalized;

        Vector3 moveDir = new(inputVector.x, 0, inputVector.y);
        transform.position += moveSpeed * Time.deltaTime * moveDir;

        isWalking = moveDir != Vector3.zero;
         
        float rotateSpeed = 10f;
        transform.forward = Vector3.Slerp(transform.forward, moveDir, Time.deltaTime * rotateSpeed);
    }

    public bool IsWalking() {
        return isWalking;
    }
}
