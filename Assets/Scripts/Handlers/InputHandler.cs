using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputHandler : MonoBehaviour
{
    public event EventHandler OnSelectPerformed;
    [SerializeField] private VoidEvent_ChannelSO inputVoidEvent_ChannelSO;
    private PlayerInputActions playerInputActions;
    private void Awake() {
        InitializeInput();
    }
    private void InitializeInput()
    {
        playerInputActions = new PlayerInputActions();
        playerInputActions.Player.Enable();
        playerInputActions.Player.Select.performed += PlayerInputActions_SelectPerformed;
    }

    private void PlayerInputActions_SelectPerformed(InputAction.CallbackContext context)
    {
        inputVoidEvent_ChannelSO.RaiseEvent(this, EventArgs.Empty);
    }
}
