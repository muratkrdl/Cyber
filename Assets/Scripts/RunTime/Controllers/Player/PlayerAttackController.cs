using System.Threading;
using Cysharp.Threading.Tasks;
using RunTime.Data.ValueObjects;
using RunTime.Events;
using RunTime.Extensions;
using UnityEngine;

public class PlayerAttackController : MonoBehaviour
{
    private CancellationTokenSource cts = new();

    private float currentAttackIndex = -1;

    private PlayerAttackData _data;

    public void SetData(PlayerAttackData  data)
    {
        _data = data;
    }

    public void Attack()
    {
        cts?.Cancel();
        cts?.Dispose();
        cts = new CancellationTokenSource();

        currentAttackIndex++;
        
        // playerFacade.SetAnimationFloat(AnimationsID.AttackIndex, currentAttackIndex);
        // playerFacade.SetAnimationTrigger(AnimationsID.Attack);
        
        if(currentAttackIndex == _data.maxCombo)
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
        await Extensions.WaitForSecondsAsync(_data.comboResetTimer, cts);
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
