using System.Threading;
using Cysharp.Threading.Tasks;
using RunTime.Data.ValueObjects;
using RunTime.Managers;
using RunTime.Objects;
using RunTime.Systems.ObjectPooling;
using UnityEngine;

namespace RunTime.Controllers.Player
{
    public class PlayerDashController : MonoBehaviour
    {
        [SerializeField] private new SpriteRenderer renderer;

        private PlayerDashData _data;
        
        private CancellationTokenSource cts = new();

        private float GetDashSpriteFrequency => _data.dashTime / _data.spriteAmount;

        public void SetData(PlayerDashData data)
        {
            _data = data;
        }

        public void StartDash()
        {
            cts?.Cancel();
            cts?.Dispose();
            cts = new CancellationTokenSource();

            StartDashSprites(renderer, _data.dashTime, GetDashSpriteFrequency).Forget();
        }

        async UniTaskVoid StartDashSprites(SpriteRenderer spriteRenderer, float time, float dashSpriteFrequency)
        {
            for (float t = 0; t < time; t += dashSpriteFrequency)
            {
                await Extensions.Extensions.WaitUntilAsync(() => !GameStateManager.Instance.GetIsGamePaused);
                await Extensions.Extensions.WaitForSecondsAsync(dashSpriteFrequency, cts);

                DashObject dashObject = DashObjectPool.Instance.GetPool.Get();
                dashObject.Initialize(spriteRenderer, transform);
            }

            // if(playerFacade.CheckGround())
            // {
            //     playerFacade.CanDash = true;
            // }
            // playerFacade.OnDash = false;
            // PlayerEvents.Instance.OnStateChange(new PlayerIdleState());
        }
    }
}
