using System.Threading;
using Cysharp.Threading.Tasks;
using RunTime.Data.ValueObjects;
using RunTime.Enums;
using RunTime.Events;
using RunTime.Helpers;
using RunTime.Managers;
using RunTime.Objects;
using RunTime.Systems.ObjectPooling;
using UnityEngine;

namespace RunTime.Controllers.Player
{
    public class PlayerDashController : MonoBehaviour
    {
        [SerializeField] private Rigidbody2D rb;
        [SerializeField] private SpriteRenderer spriteRenderer;

        private CancellationTokenSource _cts = new();
        
        private PlayerDashData _data;
        
        private float _initialGravity;
        private DashState _state = DashState.Ready;

        private float _dashFrequency;

        private void Awake()
        {
            _initialGravity = rb.gravityScale;
        }
        
        public void SetData(PlayerDashData data)
        {
            _data = data;
            _dashFrequency = _data.dashTime / _data.spriteAmount;
        }

        public void OnEnterGround()
        {
            ResetDash().Forget();
        }
        
        public void StartDash()
        {
            if (_state != DashState.Ready) return;
            
            CancelDash();
            _cts = new CancellationTokenSource();

            StartDashSprites().Forget();
        }

        private void CancelDash()
        {
            if (_cts?.IsCancellationRequested == false)
                _cts.Cancel();

            _cts?.Dispose();
            _cts = null;
        }

        private async UniTaskVoid StartDashSprites()
        {
            _state = DashState.Dashing;
            
            CoreGameEvents.Instance.onDisableGameplayInput?.Invoke();
            AnimationEvents.Instance.onTriggerAnimation(AnimationsID.Dash);

            float dashSpeed = _data.dashSpeed * Mathf.Sign(transform.localScale.x);

            rb.linearVelocity = new Vector2(dashSpeed, 0);
            rb.gravityScale = 0;

            var dashPool = DashObjectPool.Instance.GetPool;
            
            for (float t = 0; t < _data.dashTime; t += _dashFrequency)
            {
                await MyHelpers.WaitUntilAsync(() => !GameStateManager.Instance.GetIsGamePaused, _cts);
                await MyHelpers.WaitForSecondsAsync(_dashFrequency, _cts);

                if (_cts.IsCancellationRequested) break;
                
                DashObject dashObject = dashPool.Get();
                dashObject.Initialize(spriteRenderer, transform);
            }

            _state = DashState.Cooldown;
            
            rb.gravityScale = _initialGravity;
            rb.linearVelocity = Caches.Zero2;
            
            CoreGameEvents.Instance.onEnableGameplayInput?.Invoke();
            AnimationEvents.Instance.onTriggerAnimation(AnimationsID.Idle);
        }

        private async UniTaskVoid ResetDash()
        {
            await MyHelpers.WaitForSecondsAsync(_data.canDashTimer);
            await MyHelpers.WaitUntilAsync(() => !GameStateManager.Instance.GetIsGamePaused, _cts);
            _state = DashState.Ready;
        }

        private void OnDisable()
        {
            CancelDash();
        }
        
    }
}
