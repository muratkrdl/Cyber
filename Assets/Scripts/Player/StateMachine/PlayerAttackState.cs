using Cysharp.Threading.Tasks;
using RunTime.Events;
using RunTime.Helpers;
using RunTime.Managers;

public class PlayerAttackState
{
    // public void EnterState()
    // {
    //     if(!player.CanMove || !player.CheckGround())
    //     {
    //         PlayerEvents.Instance.OnStateChange.Invoke(new PlayerIdleState());
    //         return;
    //     }
    //     
    //     player.CanMove = false;
    //     player.OnAttack();
    //     ResetAttack(player).Forget();
    // }
 
    // async UniTaskVoid ResetAttack()
    // {
    //     await Extensions.WaitForSecondsAsync(player.GetTimeBtwnAttack());
    //     await Extensions.WaitUntilAsync(() => !GameStateManager.Instance.GetIsGamePaused);
    //     PlayerEvents.Instance.OnStateChange.Invoke(new PlayerIdleState());
    //     player.CanMove = true;
    // }
    
}
