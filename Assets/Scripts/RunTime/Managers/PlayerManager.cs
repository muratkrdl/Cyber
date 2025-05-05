using RunTime.Behaviours;
using RunTime.Controllers.Player;
using RunTime.Data.UnityObjects;
using RunTime.Events;
using Unity.Mathematics;
using UnityEngine;

namespace RunTime.Managers
{
    public class PlayerManager : GamePlayBehaviour
    {
        [Header("References")]
        [SerializeField] private PlayerMovementController playerMovementController;
        [SerializeField] private PlayerJumpController playerJumpController;
        [SerializeField] private PlayerAttackController playerAttackController;
        [SerializeField] private PlayerDashController playerDashController;
        [SerializeField] private PlayerAnimationController playerAnimationController;
        [SerializeField] private PlayerPhysicsController playerPhysicsController;
        
        private CD_Player _data;
        
        private void SetData()
        {
            _data = Resources.Load<CD_Player>("Data/CD_Player");
            playerMovementController.SetData(_data.PlayerMovementData);
            playerJumpController.SetData(_data.PlayerJumpData);
            playerAttackController.SetData(_data.PlayerAttackData);
            playerDashController.SetData(_data.PlayerDashData);
        }
        
        private void Awake()
        {
            SubscribeEvents();
            SetData();
        }

        private void SubscribeEvents()
        {
            InputEvents.Instance.onStartMove += OnStartMove;
            InputEvents.Instance.onStopMove += OnStopMove;
            InputEvents.Instance.onJump += OnJump;
            InputEvents.Instance.onAttack += OnAttack;
            InputEvents.Instance.onDash += OnDash;

            PlayerEvents.Instance.onEnterGround += OnEnterGround;
            PlayerEvents.Instance.onExitGround += OnExitGround;
        }

        private void OnEnterGround()
        {
            playerJumpController.OnEnterGround();
            playerDashController.OnEnterGround();
            playerAttackController.OnEnterGround();
        }
        
        private void OnExitGround()
        {
            playerAttackController.OnExitGround();
        }

        private void OnStartMove(float2 input)
        {
            playerMovementController.MoveHandle(input);
        }
        
        private void OnStopMove()
        {
            playerMovementController.StopMove();
        }
        
        private void OnJump()
        {
            playerJumpController.OnJump();
        }
        
        private void OnAttack()
        {
            playerAttackController.Attack();
        }
        
        private void OnDash()
        {
            playerDashController.StartDash();
        }

        private void UnSubscribeEvents()
        {
            InputEvents.Instance.onStartMove -= OnStartMove;
            InputEvents.Instance.onStopMove -= OnStopMove;
            InputEvents.Instance.onJump -= OnJump;
            
            InputEvents.Instance.onDash -= OnDash;
        }

        private void OnDisable()
        {
            UnSubscribeEvents();
        }
    
        protected override void GameStateManager_OnGamePause()
        {
            // myRigidbody.gravityScale = 0;
            // onPauseLinearVelocityY = myRigidbody.linearVelocityY;
            // myRigidbody.linearVelocity = Caches.Zero2;
            // animator.speed = 0;
            // PlayerEvents.Instance.OnStateChange?.Invoke(new PlayerPauseState());
        }
    
        protected override void GameStateManager_OnGameResume()
        {
            // myRigidbody.linearVelocityY = onPauseLinearVelocityY;
            // animator.speed = 1;
            // PlayerEvents.Instance.OnStateChange?.Invoke(new PlayerIdleState());
        }
    }
}
