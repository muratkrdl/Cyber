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
    
    public void OnAttack()
    {
        cts?.Cancel();
        cts?.Dispose();
        cts = new();

        currentAttackIndex++;
        playerFacade.SetAnimationFloat(Const.PlayerAnimations.FLOAT_ATTACK_INDEX, currentAttackIndex);
        playerFacade.SetAnimationTrigger(Const.PlayerAnimations.TRIGGER_ATTACK);
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

    private void OnDestroy()
    {
        cts.Cancel();
        cts.Dispose();
        PlayerEvents.Instance.OnResetAttackIndex -= PlayerAttack_OnResetAttackIndex;
    }

}
