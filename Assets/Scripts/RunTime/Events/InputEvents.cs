using RunTime.Extensions;
using Unity.Mathematics;
using UnityEngine.Events;

namespace RunTime.Events
{
    public class InputEvents : MonoSingleton<InputEvents>
    {
        public UnityAction<float2> onStartMove;
        public UnityAction onStopMove;
        public UnityAction onJump;
        public UnityAction onAttack;
        public UnityAction onDash;
        
    }
}
