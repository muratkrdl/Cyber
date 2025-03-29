using UnityEngine;

public class PlayerIdleState : IPlayerState
{
    public void EnterState(PlayerFacade player)
    {
    }
    public void ExitState(PlayerFacade player)
    {
    }
    public void FixedUpdateState(PlayerFacade player)
    {
    }
    public void UpdateState(PlayerFacade player)
    {
        if(Mathf.Abs(player.GetPlayerMovementInput().x) > 0 && player.CanMove)
        {
            PlayerEvents.Instance.OnStateChange.Invoke(new PlayerMoveState());
        }
    }
}
