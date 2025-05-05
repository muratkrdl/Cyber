using System.Threading;
using Cysharp.Threading.Tasks;
using RunTime.Data.ValueObjects;
using RunTime.Enums;
using RunTime.Events;
using RunTime.Helpers;
using RunTime.Keys;
using RunTime.Managers;
using UnityEngine;

namespace RunTime.Controllers.Player
{
    public class PlayerAttackController : MonoBehaviour
    {
        private CancellationTokenSource _cts = new();
        
        private PlayerAttackData _data;
        private AttackState _state = AttackState.UnReady;
        private int _currentAttackIndex = -1;

        public void SetData(PlayerAttackData  data)
        {
            _data = data;
        }

        private void OnEnable()
        {
            PlayerEvents.Instance.onResetAttackIndex += OnResetAttackIndex;
        }

        public void OnEnterGround()
        {
            _state = AttackState.Ready;
        }
        
        public void OnExitGround()
        {
            _state = AttackState.UnReady;
        }

        private void OnResetAttackIndex()
        {
            _currentAttackIndex = -1;
        }
        
        private void OnDisable() 
        {
            PlayerEvents.Instance.onResetAttackIndex -= OnResetAttackIndex;
            ResetCancellationToken();
        }
        
        private void ResetCancellationToken()
        {
            _cts?.Cancel();
            _cts?.Dispose();
            _cts = new CancellationTokenSource();
        }

        public void Attack()
        {
            if (_state != AttackState.Ready) return;
            
            CoreGameEvents.Instance.onDisableGameplayInput?.Invoke();
            
            _state = AttackState.Cooldown;
            ResetCancellationToken();
            _currentAttackIndex++;
        
            AnimationEvents.Instance.onFloatAnimation(new AnimationFloatParams() { Id = AnimationsID.AttackIndex, Value = _currentAttackIndex });
            AnimationEvents.Instance.onTriggerAnimation(AnimationsID.Attack);
        
            if(_currentAttackIndex == _data.maxCombo)
            {
                _currentAttackIndex = -1;
            }
            else
            {
                ResetCombo().Forget();
            }
            
            CooldownAttack().Forget();
        }

        private async UniTaskVoid CooldownAttack()
        {
            await MyHelpers.WaitForSecondsAsync(_data.timeBtwnAttack);
            await MyHelpers.WaitUntilAsync(() => !GameStateManager.Instance.GetIsGamePaused);
            CoreGameEvents.Instance.onEnableGameplayInput?.Invoke();
            _state = AttackState.Ready;
        }
    
        private async UniTaskVoid ResetCombo()
        {
            await MyHelpers.WaitForSecondsAsync(_data.comboResetTimer, _cts);
            _currentAttackIndex = -1;
        }
        
    }
}
