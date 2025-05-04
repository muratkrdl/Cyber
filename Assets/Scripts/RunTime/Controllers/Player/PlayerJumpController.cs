using RunTime.Data.ValueObjects;
using RunTime.Helpers;
using UnityEngine;

namespace RunTime.Controllers.Player
{
    public class PlayerJumpController : MonoBehaviour
    {
        [SerializeField] private Rigidbody2D rb;
        [SerializeField] private Transform feetTransform;
        
        private PlayerJumpData _data;
        
        public void SetData(PlayerJumpData data)
        {
            _data = data;
        }

        public void OnJump()
        {
            if(!CheckGround()) return;
            
            rb.linearVelocityY = 0;
            rb.AddForce(Caches.Up2 * _data.jumpForce, ForceMode2D.Impulse);
        }

        public bool CheckGround()
        {
            Collider2D isGrounded = Physics2D.OverlapCircle(feetTransform.position, _data.groundCheckSize, _data.groundLayerMask);
            return isGrounded;
        }
        
        private void OnDrawGizmos()
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(feetTransform.position, _data.groundCheckSize);
        }
        
    }
}
