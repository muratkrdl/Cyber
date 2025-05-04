using RunTime.Extensions;
using RunTime.Keys;
using UnityEngine.Events;

namespace RunTime.Events
{
    public class AnimationEvents : MonoSingleton<AnimationEvents>
    {
        public UnityAction<int> onTriggerAnimation;
        public UnityAction<AnimationBoolParams> onBoolAnimation;
        public UnityAction<AnimationFloatParams> onFloatAnimation;
    }
}