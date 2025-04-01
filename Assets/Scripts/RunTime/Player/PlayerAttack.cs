using System.Threading;
using Cysharp.Threading.Tasks;

public class PlayerAttack
{
    private CancellationTokenSource cts = new();

    private PlayerFacade playerFacade;

    private float currentAttackIndex = -1;

    public PlayerAttack(PlayerFacade playerFacade)
    {
        this.playerFacade = playerFacade;
        PlayerEvents.Instance.OnResetAttackIndex += PlayerAttack_OnResetAttackIndex;
    }
    
    public void Attack()
    {
        cts?.Cancel();
        cts?.Dispose();
        cts = new();

        currentAttackIndex++;
        playerFacade.SetAnimationFloat(AnimationsID.AttackIndex, currentAttackIndex);
        playerFacade.SetAnimationTrigger(AnimationsID.Attack);
        if(currentAttackIndex == playerFacade.GetMaxCombo())
        {
            currentAttackIndex = -1;
        }
        else
        {
            ResetCombo().Forget();
        }
    }
    
    private async UniTaskVoid ResetCombo()
    {
        await Extensions.GetUnitaskTime(playerFacade.GetComboResetTimer(), cts);
        currentAttackIndex = -1;
    }

    private void PlayerAttack_OnResetAttackIndex()
    {
        currentAttackIndex = -1;
    }

    private void OnApplicationQuit() 
    {
        cts.Cancel();
        cts.Dispose();
        PlayerEvents.Instance.OnResetAttackIndex -= PlayerAttack_OnResetAttackIndex;
    }

}
