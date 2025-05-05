using System;
using System.Threading;
using Cysharp.Threading.Tasks;
using UnityEngine;
using Random = UnityEngine.Random;

namespace RunTime.Helpers
{
    public static class MyHelpers
    {
        public static Color GetRandomColor()
        {
            return new Color(Random.Range(0f, 1f), Random.Range(0f, 1f), Random.Range(0f, 1f));
        }
        public static UniTask WaitForSecondsAsync(float time)
        {
            return UniTask.Delay(TimeSpan.FromSeconds(time));
        }
        public static UniTask WaitForSecondsAsync(float time, CancellationTokenSource cts)
        {
            return UniTask.Delay(TimeSpan.FromSeconds(time), cancellationToken: cts.Token);
        }
        public static UniTask WaitUntilAsync(Func<bool> value)
        {
            return UniTask.WaitUntil(value);
        }
        public static UniTask WaitUntilAsync(Func<bool> value, CancellationTokenSource cts)
        {
            return UniTask.WaitUntil(value, cancellationToken: cts.Token);
        }
    }
}