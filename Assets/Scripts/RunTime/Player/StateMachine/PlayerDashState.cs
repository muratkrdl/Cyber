public class PlayerDashState : IPlayerState
{
    public void EnterState(Player player)
    {
        player.CanDash = false;
        player.OnDash = true;
        
        float dashSpeed = player.GetPlayerDash().GetDashSpeed;
        if(player.transform.localScale.x < 0)
        {
            dashSpeed *= -1;
        }

        player.GetRigidbody.linearVelocityX = dashSpeed;
        player.GetRigidbody.linearVelocityY = 0;
        player.GetRigidbody.gravityScale = 0;

        player.GetPlayerAnimation().SetTrigger(Const.PlayerAnimations.TRIGGER_DASH);
        player.GetPlayerDash().StartDash();
    }
    public void ExitState(Player player)
    {
        player.GetRigidbody.linearVelocityX = 0;
        player.GetRigidbody.gravityScale = player.GetInitialGravity;
        player.GetPlayerAnimation().SetTrigger(Const.PlayerAnimations.TRIGGER_IDLE);
    }
    public void FixedUpdateState(Player player)
    {
    }
    public void UpdateState(Player player)
    {
    }
}
