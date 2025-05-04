using System;

namespace RunTime.Data.ValueObjects
{
    [Serializable]
    public struct PlayerDashData
    {
        public float dashSpeed;
        public float dashTime;
        public float spriteAmount;
    }
}