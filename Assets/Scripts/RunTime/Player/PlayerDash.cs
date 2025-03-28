using System.Threading;
using Cysharp.Threading.Tasks;
using UnityEngine;

public class PlayerDash : MonoBehaviour
{
    [Header("Settings")]
    [SerializeField] private float dashSpeed;
    [SerializeField] private float dashTime;
    [SerializeField] private float spriteAmount;

    private CancellationTokenSource cts = new();

    private Player player;

    public float GetDashSpeed
    {
        get => dashSpeed;
    }

    public float GetDashSpriteFrequency
    {
        get => dashTime / spriteAmount;
    }
    
    private void Awake() 
    {
        player = GetComponent<Player>();
    }

    public void StartDash()
    {
        cts.Cancel(); cts = new();

        StartSpriteCreator(player.GetPlayerSpriteRenderer, dashTime, GetDashSpriteFrequency).Forget();
    }

    async UniTaskVoid StartSpriteCreator(SpriteRenderer spriteRenderer, float time, float dashSpriteFrequency)
    {
        float t = 0;
        while(true)
        {
            await UniTask.WaitUntil(() => !GameStateManager.Instance.GetIsGamePaused);
            await UniTask.Delay(Extensions.GetUnitaskTime(dashSpriteFrequency), cancellationToken: cts.Token);

            DashObject dashObject = DashObjectPool.Instance.GetPool.Get();
            dashObject.Initialize(spriteRenderer, player.transform);
 
            t += dashSpriteFrequency;
            if(t >= time)
            {
                break;
            }
        }

        player.GetPlayerEvents().OnStateChange(new PlayerIdleState());
    }

}
