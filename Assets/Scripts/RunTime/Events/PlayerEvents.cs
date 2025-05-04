using System;
using RunTime.Extensions;
using UnityEngine;

namespace RunTime.Events
{
    public class PlayerEvents : MonoSingleton<PlayerEvents>
    {
        // public Action<IPlayerState> OnStateChange;
        public Action OnResetAttackIndex;

    }
}
