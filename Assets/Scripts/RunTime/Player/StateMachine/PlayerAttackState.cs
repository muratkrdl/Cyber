using System;
using Cysharp.Threading.Tasks;

public class PlayerAttackState : IPlayerState
{
    public void EnterState(Player player)
    {
        if(!player.CanMove || !player.GetPlayerJump().CheckGround())
        {
            player.GetPlayerEvents().OnStateChange.Invoke(new PlayerIdleState());
            return;
        }
        player.CanMove = false;
        player.GetPlayerAttack().OnAttack();
        ResetAttack(player).Forget();
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

    async UniTaskVoid ResetAttack(Player player)
    {
        await Extensions.GetUnitaskTime(player.GetPlayerAttack().GetTimeBtwnAttack);
        player.CanMove = true;
        player.GetPlayerEvents().OnStateChange.Invoke(new PlayerIdleState());
    }
    
}
