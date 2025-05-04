using RunTime.Helpers;
using RunTime.Managers;
using UnityEngine;

namespace RunTime.Controllers.Player
{
    public class PlayerAnimationController : MonoBehaviour
    {
        [SerializeField] private Animator animator;
        [SerializeField] private Rigidbody2D rb;
        
        private Vector3 initialScale;

        public void SetAnimationValues()
        {
            //if(GameStateManager.Instance.GetIsGamePaused || playerFacade.OnDash) return;

            float moveInputX = rb.linearVelocityX;
            float linearVelocityY = rb.linearVelocityY;

            animator.SetFloat(AnimationsID.LinearVelocityY, linearVelocityY);

            if(Mathf.Abs(moveInputX) > Mathf.Epsilon)
            {
                moveInputX = Mathf.Sign(moveInputX);
            }

            // if(playerFacade.CanMove)
            {
                if(moveInputX != 0)
                {
                    transform.localScale = new Vector3(moveInputX, initialScale.y, initialScale.z);
                }
            
                animator.SetFloat(AnimationsID.Speed, Mathf.Abs(moveInputX));
            }
        }

        public void SetTrigger(int triggerID)
        {
            animator.SetTrigger(triggerID);
        }
        public void SetBool(int boolID, bool value)
        {
            animator.SetBool(boolID, value);
        }
        public void SetFloat(int floatID, float value)
        {
            animator.SetFloat(floatID, value);
        }
    }
}
