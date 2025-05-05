using RunTime.Events;
using RunTime.Helpers;
using RunTime.Keys;
using UnityEngine;

namespace RunTime.Controllers.Player
{
    public class PlayerAnimationEventsController : MonoBehaviour
    {
        public void AnimEvent_StartFalling()
        {
            AnimationEvents.Instance.onBoolAnimation(new AnimationBoolParams()
                { Id = AnimationsID.IsFalling, Value = true });
        }

        public void AnimEvent_StopFalling()
        {
            AnimationEvents.Instance.onBoolAnimation(new AnimationBoolParams()
                { Id = AnimationsID.IsFalling, Value = false });
        }

        public void AnimEvent_Attack()
        {
            // TODO : Implement sphere overlap
            
        }

    }
}
