using System.Threading;
using Cysharp.Threading.Tasks;
using RunTime.Data.ValueObjects;
using RunTime.Events;
using RunTime.Extensions;
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

        private PlayerDashData _data;
        private float _initialGravity;
        
        private CancellationTokenSource _cts = new();

        private float GetDashSpriteFrequency => _data.dashTime / _data.spriteAmount;

        private void Awake()
        {
            _initialGravity = rb.gravityScale;
        }

        public void SetData(PlayerDashData data)
        {
            _data = data;
        }

        public void StartDash()
        {
            CancelDash();
            _cts = new CancellationTokenSource();

            StartDashSprites(spriteRenderer).Forget();
        }
        
        public void CancelDash()
        {
            if (_cts == null) return;
            
            _cts.Cancel();
            _cts.Dispose();
            _cts = null;
        }

        private async UniTaskVoid StartDashSprites(SpriteRenderer spriteRenderer)
        {
            CoreGameEvents.Instance.onDisableInput?.Invoke();
            AnimationEvents.Instance.onTriggerAnimation(AnimationsID.Dash);
            
            float dashSpeed = transform.localScale.x switch
            {
                >0 => _data.dashSpeed,
                _ => _data.dashSpeed * -1
            };

            rb.linearVelocity = new Vector2(dashSpeed, 0);
            rb.gravityScale = 0;
            
            for (float t = 0; t < _data.dashTime; t += GetDashSpriteFrequency)
            {
                await MyExtensions.WaitUntilAsync(() => !GameStateManager.Instance.GetIsGamePaused, _cts);
                await MyExtensions.WaitForSecondsAsync(GetDashSpriteFrequency, _cts);

                if (_cts.IsCancellationRequested) break;
                
                DashObject dashObject = DashObjectPool.Instance.GetPool.Get();
                dashObject.Initialize(spriteRenderer, transform);
            }

            // if(playerFacade.CheckGround())
            // {
            //     playerFacade.CanDash = true;
            // }
            // playerFacade.OnDash = false;
            // PlayerEvents.Instance.OnStateChange(new PlayerIdleState());
            
            rb.gravityScale = _initialGravity;
            rb.linearVelocity = Caches.Zero2;
            CoreGameEvents.Instance.onEnableInput?.Invoke();
            AnimationEvents.Instance.onTriggerAnimation(AnimationsID.Idle);
        }
    }
}
