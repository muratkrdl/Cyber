using System;
using DG.Tweening;

namespace RunTime.Data.ValueObjects.DashObject
{
    [Serializable]
    public struct DashObjData
    {
        public Ease EaseMode;
        public float Duration;
    }
}