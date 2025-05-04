using RunTime.Data.ValueObjects;
using UnityEngine;

namespace RunTime.Data.UnityObjects
{
    [CreateAssetMenu(fileName = "CD_Player", menuName = "SO/CD_Player", order = 0)]
    public class CD_Player : ScriptableObject
    {
        public PlayerMovementData  PlayerMovementData;
        public PlayerJumpData PlayerJumpData;
        public PlayerAttackData PlayerAttackData;
        public PlayerDashData PlayerDashData;
    }
}