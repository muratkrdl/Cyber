public class PlayerJumpState : IPlayerState
{
    public void EnterState(PlayerFacade player)
    {
        if(player.CanMove)
        {
            player.PlayerJump();
        }

        PlayerEvents.Instance.OnStateChange.Invoke(new PlayerIdleState());
    }
    public void ExitState(PlayerFacade player)
    {
    }
    public void FixedUpdateState(PlayerFacade player)
    {
    }
    public void UpdateState(PlayerFacade player)
    {
        // if(player.GetPlayerJump().CheckGround())
        // {
        //     player.GetPlayerEvents().OnStateChange.Invoke(new PlayerIdleState());
        // }
    }
}
