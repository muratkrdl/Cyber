using System;
using UnityEngine;
using Random = UnityEngine.Random;

public static class Extensions
{
    public static Color GetRandomColor()
    {
        return new Color(Random.Range(0f, 1f), Random.Range(0f, 1f), Random.Range(0f, 1f));
    }
    public static TimeSpan GetUnitaskTime(float time)
    {
        return TimeSpan.FromSeconds(time);
    }
}
