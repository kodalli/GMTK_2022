using System;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Events;

[CreateAssetMenu(fileName = "InputProvider", menuName = "Input/Input Provider")]
public class InputProvider : ScriptableObject, IInputProvider, PlayerInput.IGameplayActions {
    // Gameplay

    private PlayerInput GameInput { get; set; }
    public InputState inputState;
    public event UnityAction<Vector2> MousePosEvent;
    public event UnityAction ShootEventStart;
    public event UnityAction ShootEventEnd;
    public event UnityAction InteractionCancelledEvent;
    public event UnityAction InteractionStartedEvent;


    private void OnEnable() {
        GameInput ??= new PlayerInput();
        GameInput.Gameplay.SetCallbacks(this);
    }

    public void OnMove(InputAction.CallbackContext context) {
        inputState.movementDirection = context.ReadValue<Vector2>();
    }

    public void OnInteract(InputAction.CallbackContext context) {
        switch (context.phase) {
            case InputActionPhase.Started:
                inputState.interactClicked = true;
                inputState.interactReleased = false;
                inputState.isInteracting = true;
                inputState.holdTimer = 0;
                InteractionStartedEvent?.Invoke();
                break;
            case InputActionPhase.Canceled:
                inputState.interactClicked = false;
                inputState.interactReleased = true;
                inputState.isInteracting = false;
                inputState.holdTimer = 0;
                InteractionCancelledEvent?.Invoke();
                break;
        }
    }

    public void OnMouse(InputAction.CallbackContext context) {
        MousePosEvent?.Invoke(context.ReadValue<Vector2>());
        inputState.mouseDirection = Camera.main.ScreenToWorldPoint(context.ReadValue<Vector2>());
    }

    public void OnShoot(InputAction.CallbackContext context) {
        switch (context.phase) {
            case InputActionPhase.Started:
                inputState.leftMouseButtonClicked = true;
                inputState.leftMouseButtonReleased = false;
                ShootEventStart?.Invoke();
                break;
            case InputActionPhase.Canceled:
                inputState.leftMouseButtonClicked = false;
                inputState.leftMouseButtonReleased = true;
                ShootEventEnd?.Invoke();
                break;
        }
    }

    public static implicit operator InputState(InputProvider provider) => provider.GetState();
    public static implicit operator Vector2(InputProvider provider) => provider.GetState().movementDirection.normalized;

    public InputState GetState() {
        return new InputState {
            movementDirection = inputState.movementDirection,

            interactClicked = inputState.interactClicked,
            interactReleased = inputState.interactReleased,
            isInteracting = inputState.isInteracting,

            leftMouseButtonClicked = inputState.leftMouseButtonClicked,
            leftMouseButtonReleased = inputState.leftMouseButtonReleased
        };
    }

    public void EnableInput() {
        GameInput.Gameplay.Enable();
        Helper.CustomLog("Gameplay Input Enabled", LogColor.White);
    }

    public void DisableInput() {
        GameInput.Gameplay.Disable();
        Helper.CustomLog("Gameplay Input Disabled", LogColor.White);
    }
}