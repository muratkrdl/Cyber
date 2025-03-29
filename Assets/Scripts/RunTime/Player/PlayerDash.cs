using System.Threading;
using Cysharp.Threading.Tasks;
using UnityEngine;

public class PlayerDash
{
    private CancellationTokenSource cts = new();

    private PlayerFacade playerFacade;

    public PlayerDash(PlayerFacade playerFacade)
    {
        this.playerFacade = playerFacade;
    }

    public void StartDash()
    {
        cts?.Cancel();
        cts?.Dispose();
        cts = new();

        StartDashSprites(playerFacade.GetPlayerSpriteRenderer(), playerFacade.GetDashTime(), playerFacade.GetDashSpriteFrequency()).Forget();
    }

    async UniTaskVoid StartDashSprites(SpriteRenderer spriteRenderer, float time, float dashSpriteFrequency)
    {
        for (float t = 0; t < time; t += dashSpriteFrequency)
        {
            await Extensions.WaitUntil(!GameStateManager.Instance.GetIsGamePaused);
            await Extensions.GetUnitaskTime(dashSpriteFrequency, cts);

            DashObject dashObject = DashObjectPool.Instance.GetPool.Get();
            dashObject.Initialize(spriteRenderer, playerFacade.transform);
        }

        if(playerFacade.CheckGround())
        {
            playerFacade.CanDash = true;
        }
        playerFacade.OnDash = false;
        PlayerEvents.Instance.OnStateChange(new PlayerIdleState());
    }

}
