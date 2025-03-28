using System;
using System.Threading;
using Cysharp.Threading.Tasks;
using UnityEngine;
using Random = UnityEngine.Random;

public static class Extensions
{
    public static Color GetRandomColor()
    {
        return new Color(Random.Range(0f, 1f), Random.Range(0f, 1f), Random.Range(0f, 1f));
    }
    public static UniTask GetUnitaskTime(float time)
    {
        return UniTask.Delay(TimeSpan.FromSeconds(time));
    }
    public static UniTask GetUnitaskTime(float time, CancellationTokenSource cts)
    {
        return UniTask.Delay(TimeSpan.FromSeconds(time), cancellationToken: cts.Token);
    }
    public static UniTask WaitUntil(bool value)
    {
         return UniTask.WaitUntil(() => value);
    }
}
