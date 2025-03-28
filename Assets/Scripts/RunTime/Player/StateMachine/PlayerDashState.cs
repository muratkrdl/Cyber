public class PlayerDashState : IPlayerState
{
    public void EnterState(Player player)
    {
        if(!player.CanDash)
        {
            return;
        }

        player.GetPlayerAnimation().SetTrigger(Const.PlayerAnimations.TRIGGER_DASH);
        player.GetPlayerDash().StartDash();
        float dashSpeed = player.GetPlayerDash().GetDashSpeed;

        if(player.transform.localScale.x < 0)
        {
            dashSpeed *= -1;
        }

        player.GetRigidbody.linearVelocityX = dashSpeed;
        player.GetRigidbody.linearVelocityY = 0;
        player.GetRigidbody.gravityScale = 0;
    }
    public void ExitState(Player player)
    {
        player.GetRigidbody.linearVelocityX = 0;
        player.GetRigidbody.gravityScale = player.GetInitialGravity;
    }
    public void FixedUpdateState(Player player)
    {
    }
    public void UpdateState(Player player)
    {
    }
}
