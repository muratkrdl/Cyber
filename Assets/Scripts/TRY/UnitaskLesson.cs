using System;
using System.Collections;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace TRY
{
    public class UnitaskLesson : MonoBehaviour
    {
        private bool tryBool = false;

        private IEnumerator TryCoRoutine()
        {
            Debug.Log("Before");
            yield return new WaitForSeconds(1);
            yield return new WaitUntil(() => tryBool);
            Debug.Log("After");
        }

        async UniTaskVoid TryUnitask()
        {
            Debug.Log("Before");
            await UniTask.Delay(1000);
            await UniTask.Delay(TimeSpan.FromSeconds(1));
            await UniTask.WaitUntil(() => tryBool);
            await UnitaskClass.WaitUntil(tryBool);
            await UnitaskClass.FromSeconds(1);
            Debug.Log("After");
        }
        
        
    }
    
    public static class UnitaskClass
    {
        public static UniTask FromSeconds(float waitTime)
        {
            return UniTask.Delay(TimeSpan.FromSeconds(waitTime));
        }

        public static UniTask WaitUntil(bool condition)
        {
            return UniTask.WaitUntil(() => condition);
        }
    }
}