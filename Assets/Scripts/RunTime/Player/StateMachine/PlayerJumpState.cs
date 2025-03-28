public class PlayerJumpState : IPlayerState
{
    public void EnterState(Player player)
    {
        if(player.CanMove)
        {
            player.GetPlayerJump().OnJump();
        }

        player.GetPlayerEvents().OnStateChange.Invoke(new PlayerIdleState());
    }
    public void ExitState(Player player)
    {
    }
    public void FixedUpdateState(Player player)
    {
    }
    public void UpdateState(Player player)
    {
    }
}
