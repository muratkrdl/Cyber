using Cysharp.Threading.Tasks;
using RunTime.Behaviours;
using RunTime.Controllers.Player;
using RunTime.Data.UnityObjects;
using RunTime.Events;
using RunTime.Helpers;
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
        [SerializeField] private PlayerAnimationEventsController playerAnimationEventsController;
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
        }

        private void OnStartMove(float2 input)
        {
            playerMovementController.MoveHandle(input);
        }
        
        private void OnStopMove()
        {
            playerMovementController.StopMove();
        }

        private void UnSubscribeEvents()
        {
            InputEvents.Instance.onStartMove -= OnStartMove;
            InputEvents.Instance.onStopMove -= OnStopMove;
        }

        private void OnDisable()
        {
            UnSubscribeEvents();
        }


        /*
        Move
        public Vector2 GetPlayerMovementInput() => playerInputHandle.GetMoveInput;
    
        Jump
        public void PlayerJump() => playerJump.OnJump();
        public bool CheckGround() => playerJump.CheckGround();
    
        Attack
        public void OnAttack() => playerAttack.Attack();
    
        Animation
        public void SetAnimationFloat(int floatID, float value) => playerAnimation.SetFloat(floatID, value);
        public void SetAnimationBool(int boolID, bool value) => playerAnimation.SetBool(boolID, value);
        public void SetAnimationTrigger(int triggerID) => playerAnimation.SetTrigger(triggerID);
        */
        
        
        
        
        /*
        async UniTaskVoid SetCanDashTrue()
        {
            float dashResetTimer = .09f;
            await Extensions.Extensions.WaitForSecondsAsync(dashResetTimer);
            canDash = true;
        }
        */
    
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
