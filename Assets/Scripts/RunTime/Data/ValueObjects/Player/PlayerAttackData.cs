using System;

namespace RunTime.Data.ValueObjects
{
    [Serializable]
    public struct PlayerAttackData
    {
        public float comboResetTimer;
        public float timeBtwnAttack;
        public int maxCombo;
    }
}