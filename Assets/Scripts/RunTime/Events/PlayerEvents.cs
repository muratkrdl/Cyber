using System;
using RunTime.Extensions;
using UnityEngine.Events;

namespace RunTime.Events
{
    public class PlayerEvents : MonoSingleton<PlayerEvents>
    {
        public UnityAction onEnterGround;
        public UnityAction onExitGround;
        public Action onResetAttackIndex;

    }
}
