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
        cts?.Cancel();
        cts?.Dispose();
        cts = new();

        StartDashSprites(player.GetPlayerSpriteRenderer, dashTime, GetDashSpriteFrequency).Forget();
    }

    async UniTaskVoid StartDashSprites(SpriteRenderer spriteRenderer, float time, float dashSpriteFrequency)
    {
        for (float t = 0; t < time; t += dashSpriteFrequency)
        {
            await Extensions.WaitUntil(!GameStateManager.Instance.GetIsGamePaused);
            await Extensions.GetUnitaskTime(dashSpriteFrequency, cts);

            DashObject dashObject = DashObjectPool.Instance.GetPool.Get();
            dashObject.Initialize(spriteRenderer, player.transform);
        }

        if(player.GetPlayerJump().CheckGround())
        {
            player.CanDash = true;
        }
        player.OnDash = false;
        player.GetPlayerEvents().OnStateChange(new PlayerIdleState());
    }

}
