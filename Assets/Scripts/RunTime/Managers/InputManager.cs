using RunTime.Events;
using RunTime.Helpers;
using UnityEngine;
using UnityEngine.InputSystem;

namespace RunTime.Managers
{
    public class InputManager : MonoBehaviour
    {
        private PlayerInputActions myPlayerInput;

        private Vector2 moveInput;

        public Vector2 GetMoveInput => moveInput;

        private void Awake()
        {
            myPlayerInput = new PlayerInputActions();
            myPlayerInput.Enable();

            SubscribeEvents();
        }

        private void SubscribeEvents()
        {
            myPlayerInput.Player.Move.started += MoveActionStart;
            myPlayerInput.Player.Move.canceled += MoveActionCancel;
            
            myPlayerInput.Player.Jump.started += JumpActionStart;
            
            myPlayerInput.Player.Attack.started += AttackActionStart;
            
            myPlayerInput.Player.Dash.started += DashActionStart;
            
            CoreGameEvents.Instance.onGamePause += PauseActionStart;
            CoreGameEvents.Instance.onGameResume += ResumeActionStart;
        }

        private void MoveActionStart(InputAction.CallbackContext context)
        {
            InputEvents.Instance.onStartMove?.Invoke(context.ReadValue<Vector2>());
        }

        private void MoveActionCancel(InputAction.CallbackContext context)
        {
            InputEvents.Instance.onStopMove?.Invoke();
        }

        private void JumpActionStart(InputAction.CallbackContext context)
        {
            InputEvents.Instance.onJump?.Invoke();
        }

        private void AttackActionStart(InputAction.CallbackContext context)
        {
            InputEvents.Instance.onAttack?.Invoke();
        }

        private void DashActionStart(InputAction.CallbackContext context)
        {
            InputEvents.Instance.onDash?.Invoke();
        }
        
        private void PauseActionStart()
        {
            myPlayerInput.Disable();
        }
        
        private void ResumeActionStart()
        {
            myPlayerInput.Enable();
        }
        
        private void UnSubscribeEvents()
        {
            myPlayerInput.Player.Move.started -= MoveActionStart;
            myPlayerInput.Player.Move.canceled -= MoveActionCancel;
            
            myPlayerInput.Player.Jump.started -= JumpActionStart;
            
            myPlayerInput.Player.Attack.started -= AttackActionStart;
            
            myPlayerInput.Player.Dash.started -= DashActionStart;
            
            CoreGameEvents.Instance.onGamePause -= PauseActionStart;
            CoreGameEvents.Instance.onGameResume -= ResumeActionStart;
        }

        private void OnDisable()
        {
            UnSubscribeEvents();

            myPlayerInput.Disable();
        }

    }
}
