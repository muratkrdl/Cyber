using Unity.Mathematics;
using UnityEngine;

public class PlayerMoveState : IPlayerState
{
    public void EnterState(Player player)
    {
    }
    public void ExitState(Player player)
    {
        player.GetPlayerMovement().SetLinearVelocityXToZero();
    }
    public void FixedUpdateState(Player player)
    {
        player.GetPlayerMovement().MoveHandle();
    }
    public void UpdateState(Player player)
    {
        if(Mathf.Abs(player.GetHorizontalMovementInput()) < Mathf.Epsilon)
        {
            player.GetPlayerEvents().OnStateChange?.Invoke(new PlayerIdleState());
        }
    }
}
