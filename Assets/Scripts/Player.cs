using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Player : MonoBehaviour {
    [SerializeField] private float speed = 8f;
    [SerializeField] private GameInput gameInput;
    [SerializeField] private LayerMask countersLayerMask;

    private bool isWalking;
    private Vector3 lastInteractionDir;
    private void Update() {
        HandleMovement();
        HandleInteractions();
    }

    public bool IsWalking() {
        return isWalking;
    }

    private void HandleInteractions() {
        Vector2 inputVector = gameInput.GetMovementVectorNormalized();
        Vector3 movementDirection = new Vector3(inputVector.x, 0f, inputVector.y);

        if (movementDirection != Vector3.zero) {
            lastInteractionDir = movementDirection;
        }

        float interactDistance = 2f;
        if (Physics.Raycast(transform.position, lastInteractionDir, out RaycastHit raycastHit ,interactDistance, countersLayerMask)) {
            if (raycastHit.transform.TryGetComponent(out ClearCounter clearCounter)) {
                clearCounter.Interact();
            }
        }
    }

    private void HandleMovement() {
        Vector2 inputVector = gameInput.GetMovementVectorNormalized();
        Vector3 movementDirection = new Vector3(inputVector.x, 0f, inputVector.y);

        float moveDistance = speed * Time.deltaTime;
        float playerRadius = 0.7f;
        float playerHeight = 2f;
        bool canMove = !Physics.CapsuleCast(transform.position, transform.position + Vector3.up * playerHeight, playerRadius, movementDirection, moveDistance);

        if (!canMove) {
            Vector3 moveDirX = new Vector3(movementDirection.x, 0f, 0f).normalized;
            canMove = !Physics.CapsuleCast(transform.position, transform.position + Vector3.up * playerHeight, playerRadius, moveDirX, moveDistance);

            if (canMove) {
                // Can move only in x direction
                movementDirection = moveDirX;
            } else {
                Vector3 moveDirZ = new Vector3(0f, 0f, movementDirection.z).normalized;
                canMove = !Physics.CapsuleCast(transform.position, transform.position + Vector3.up * playerHeight, playerRadius, moveDirZ, moveDistance);

                if (canMove) {
                    // Can move only in z direction
                    movementDirection = moveDirZ;
                } else {
                    // Can't move in any direction
                    movementDirection = Vector3.zero;
                }
            }
        }

        if (canMove) {
            transform.position += movementDirection * moveDistance;
        }

        isWalking = movementDirection != Vector3.zero;

        float rotateSpeed = 10f;
        transform.forward = (Vector3.Slerp(transform.forward, movementDirection, Time.deltaTime * rotateSpeed));
    }
}
