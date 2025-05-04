using RunTime.Helpers;
using UnityEngine;

namespace RunTime.Controllers.Player
{
    public class PlayerAnimationEventsController : MonoBehaviour
    {
        public void AnimEvent_StartFalling()
        {
            // player.SetAnimationBool(AnimationsID.IsFalling, true);
        }

        public void AnimEvent_StopFalling()
        {
            // player.SetAnimationBool(AnimationsID.IsFalling, false);
        }

        public void AnimEvent_Attack()
        {
        
        }

        public void AnimEvent_OnTouchGround()
        {
            // player.CanDash = true;
        }

    }
}
