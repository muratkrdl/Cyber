using System;
using UnityEngine;
using UnityEngine.InputSystem;

public struct FramePlayerInput
{
    public Vector2 MoveInput;
}

public class PlayerInputHandle : MonoBehaviour
{
    private PlayerInputActions myPlayerInput;

    private Vector2 moveInput;

    public Vector2 GetMoveInput => moveInput;

    private void Awake()
    {
        myPlayerInput = new();
        myPlayerInput.Enable();

        myPlayerInput.Player.Move.performed += MoveActionStart;
        myPlayerInput.Player.Move.canceled += MoveActionCancel;

        myPlayerInput.Player.Jump.started += JumpActionStart;

        myPlayerInput.Player.Attack.started += AttackActionStart;

        myPlayerInput.Player.Dash.started += DashActionStart;




        myPlayerInput.Player.Pause.started += PauseActionStart;
        myPlayerInput.Player.Resume.started += ResumeActionStart;
    }

    private void PauseActionStart(InputAction.CallbackContext context)
    {
        GameStateManager.Instance.OnGamePause?.Invoke();
    }
    private void ResumeActionStart(InputAction.CallbackContext context)
    {
        GameStateManager.Instance.OnGameResume?.Invoke();
    }



    private void MoveActionStart(InputAction.CallbackContext context) => moveInput = context.ReadValue<Vector2>();
    private void MoveActionCancel(InputAction.CallbackContext context) => moveInput = Vector2.zero;

    private void JumpActionStart(InputAction.CallbackContext context) => PlayerEvents.Instance.OnStateChange?.Invoke(new PlayerJumpState());

    private void AttackActionStart(InputAction.CallbackContext context) => PlayerEvents.Instance.OnStateChange?.Invoke(new PlayerAttackState());
    private void DashActionStart(InputAction.CallbackContext context) => PlayerEvents.Instance.OnStateChange?.Invoke(new PlayerDashState());

    private void OnDisable()
    {
        myPlayerInput.Player.Move.performed -= MoveActionStart;
        myPlayerInput.Player.Move.canceled -= MoveActionCancel;

        myPlayerInput.Player.Jump.started -= JumpActionStart;

        myPlayerInput.Player.Attack.started -= AttackActionStart;

        myPlayerInput.Player.Dash.started -= DashActionStart;

        myPlayerInput.Disable();
    }

}
