using UnityEngine;

public class PlayerMoveState : IPlayerState
{
    public void EnterState(PlayerFacade player)
    {
    }
    public void ExitState(PlayerFacade player)
    {
        player.SetLinearVelocityXToZero();
    }
    public void FixedUpdateState(PlayerFacade player)
    {
        player.GetPlayerMovement().MoveHandle();
    }
    public void UpdateState(PlayerFacade player)
    {
        if(Mathf.Abs(player.GetPlayerMovementInput().x) < Mathf.Epsilon)
        {
            PlayerEvents.Instance.OnStateChange?.Invoke(new PlayerIdleState());
        }
    }
}
