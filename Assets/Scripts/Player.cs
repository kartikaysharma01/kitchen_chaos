using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Rendering.Universal;

public class Player : MonoBehaviour {

    public static Player Instance {private set;  get;}
    [SerializeField] private float moveSpeed;
    [SerializeField] private GameInput gameInput;
    [SerializeField] private LayerMask couterLayerMask;
    private Vector3 lastMoveDir;
    private bool isWalking;
    private ClearCounter selectedCounter;
    public event EventHandler<OnSeledtedCounterChangeArgs> OnSeledtedCounterChange;
    public class OnSeledtedCounterChangeArgs : EventArgs {
        public ClearCounter selectedCounter;
    }

    private void Awake() {
        if (Instance != null) {
            Debug.LogError("Player Instance has already been set. Something has gone wrong");
        }
        Instance = this;
    }
    private void Start() {
        gameInput.OnInteractAction += OnInteractAction_Listner;

    }

    private void OnInteractAction_Listner(object sender, EventArgs args) {
        if (selectedCounter != null) {
            selectedCounter.Interact();
        }
    }

    private void Update() {
        Vector2 inputVector = gameInput.GetNormalisedMovementVector();

        Vector3 moveDir = new(inputVector.x, 0, inputVector.y);

        HandlePlayerMovement(moveDir);
        HandleCounterInteractions(moveDir);
    }

    private void HandleCounterInteractions(Vector3 moveDir) {
        
        if (isWalking) {
            lastMoveDir = moveDir;
        }

        float interactionDistance = 2f;
        if (Physics.Raycast(transform.position, lastMoveDir, out RaycastHit raycastHit, interactionDistance, couterLayerMask)) {
            if (raycastHit.transform.TryGetComponent(out ClearCounter clearCounter)) {
                SetSelectedCounter(clearCounter);
            }
            else {
                SetSelectedCounter(null);
            }
        }
        else {
            SetSelectedCounter(null);
        }

    }

    private void HandlePlayerMovement(Vector3 moveDir) {
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

    private void SetSelectedCounter(ClearCounter selectedCounter) {
        this.selectedCounter = selectedCounter;

        OnSeledtedCounterChange?.Invoke(this, new OnSeledtedCounterChangeArgs{
            selectedCounter = selectedCounter
        });

    }
 
}
