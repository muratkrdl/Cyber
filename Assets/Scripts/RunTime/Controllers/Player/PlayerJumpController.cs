using RunTime.Data.ValueObjects;
using RunTime.Helpers;
using UnityEngine;

namespace RunTime.Controllers.Player
{
    public class PlayerJumpController : MonoBehaviour
    {
        [SerializeField] private Transform feetTransform;
        
        private PlayerJumpData _data;
        
        public void SetData(PlayerJumpData data)
        {
            _data = data;
        }

        public void OnJump()
        {
            if(!CheckGround()) return;

            // playerFacade.SetLinearVelocityYToZero();
            // playerFacade.AddForce(Caches.Up2 * _jumpForce, ForceMode2D.Impulse);
            // playerFacade.SetAnimationTrigger(AnimationsID.Jump);
        }

        private bool CheckGround()
        {
            Collider2D isGrounded = Physics2D.OverlapCircle(feetTransform.position, _data.groundCheckSize, _data.groundLayerMask);

            if(isGrounded)
            {
                // playerFacade.SetAnimationBool(AnimationsID.IsFalling, false);
            }
        
            return isGrounded;
        }
        
        private void OnDrawGizmos() 
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(feetTransform.position, _data.groundCheckSize);
        }
        
    }
}
