using UnityEngine;

public class PlayerIdleState : IPlayerState
{
    public void EnterState(Player player)
    {
    }
    public void ExitState(Player player)
    {
    }
    public void FixedUpdateState(Player player)
    {
    }
    public void UpdateState(Player player)
    {
        if(Mathf.Abs(player.GetHorizontalMovementInput()) > 0 && player.CanMove)
        {
            player.GetPlayerEvents().OnStateChange.Invoke(new PlayerMoveState());
        }
    }
}
