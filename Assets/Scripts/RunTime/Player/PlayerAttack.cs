using System;
using System.Threading;
using Cysharp.Threading.Tasks;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    [SerializeField] private float comboResetTimer;
    [SerializeField] private float timeBtwnAttack;
    [SerializeField] private int maxCombo;

    private CancellationTokenSource cts = new();

    private Player player;

    private float currentAttackIndex = -1;

    public float GetTimeBtwnAttack
    {
        get => timeBtwnAttack;
    }

    private void Awake()
    {
        player = GetComponent<Player>();

        player.GetPlayerEvents().OnResetAttackIndex += PlayerAttack_OnResetAttackIndex;
    }
    
    public void OnAttack()
    {
        cts?.Cancel();
        cts?.Dispose();
        cts = new();

        currentAttackIndex++;
        player.GetPlayerAnimation().SetFloat(Const.PlayerAnimations.FLOAT_ATTACK_INDEX, currentAttackIndex);
        player.GetPlayerAnimation().SetTrigger(Const.PlayerAnimations.TRIGGER_ATTACK);
        if(currentAttackIndex == maxCombo)
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
        await Extensions.GetUnitaskTime(comboResetTimer, cts);
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
        player.GetPlayerEvents().OnResetAttackIndex -= PlayerAttack_OnResetAttackIndex;
    }

}
