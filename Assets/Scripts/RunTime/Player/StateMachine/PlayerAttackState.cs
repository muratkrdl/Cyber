using Cysharp.Threading.Tasks;

public class PlayerAttackState : IPlayerState
{
    public void EnterState(PlayerFacade player)
    {
        if(!player.CanMove || !player.CheckGround())
        {
            PlayerEvents.Instance.OnStateChange.Invoke(new PlayerIdleState());
            return;
        }
        
        player.CanMove = false;
        player.OnAttack();
        ResetAttack(player).Forget();
    }
    public void ExitState(PlayerFacade player)
    {
    }
    public void FixedUpdateState(PlayerFacade player)
    {
    }
    public void UpdateState(PlayerFacade player)
    {
    }

    async UniTaskVoid ResetAttack(PlayerFacade player)
    {
        await Extensions.GetUnitaskTime(player.GetTimeBtwnAttack());
        await Extensions.WaitUntil(!GameStateManager.Instance.GetIsGamePaused);
        PlayerEvents.Instance.OnStateChange.Invoke(new PlayerIdleState());
        player.CanMove = true;
    }
    
}
