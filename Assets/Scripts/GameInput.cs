using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class GameInput : MonoBehaviour {
    private PlayerInputActions playerInputActions;
    public event EventHandler OnInteract;
    public event EventHandler OnRemove;
    public event EventHandler OnInteractAlternate;

    private void Awake() {
        playerInputActions = new PlayerInputActions();
        playerInputActions.Player.Enable();

        playerInputActions.Player.Interact.performed += Interact_performed;
        playerInputActions.Player.InteractAlternate.performed += InteractAlternate_performed;
        playerInputActions.Player.Remove.performed += Remove_performed;
    }

    private void InteractAlternate_performed(InputAction.CallbackContext context) {
        OnInteractAlternate?.Invoke(this, EventArgs.Empty);
    }

    private void Remove_performed(InputAction.CallbackContext context) {
        OnRemove?.Invoke(this, EventArgs.Empty);
    }

    private void Interact_performed(UnityEngine.InputSystem.InputAction.CallbackContext obj) {
        OnInteract?.Invoke(this, EventArgs.Empty);
    }

    public Vector2 GetMovementVectorNormalized() {
        Vector2 inputVector = playerInputActions.Player.Move.ReadValue<Vector2>();

        inputVector = inputVector.normalized;
        return inputVector;
    }
}
