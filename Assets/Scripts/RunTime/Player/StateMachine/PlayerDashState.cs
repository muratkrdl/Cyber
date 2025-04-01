public class PlayerDashState : IPlayerState
{
    public void EnterState(PlayerFacade player)
    {
        player.CanDash = false;
        player.OnDash = true;
        
        float dashSpeed = player.GetDashSpeed();
        if(player.transform.localScale.x < 0)
        {
            dashSpeed *= -1;
        }

        player.SetLinearVelocityX(dashSpeed);
        player.SetLinearVelocityYToZero();
        player.SetGravityScaleToZero();

        player.SetAnimationTrigger(AnimationsID.Dash);
        player.OnStartDash();
    }
    public void ExitState(PlayerFacade player)
    {
        player.SetLinearVelocityXToZero();
        player.SetGravityScaleToInitial();
        player.SetAnimationTrigger(AnimationsID.Idle);
    }
    public void FixedUpdateState(PlayerFacade player)
    {
    }
    public void UpdateState(PlayerFacade player)
    {
    }
}
