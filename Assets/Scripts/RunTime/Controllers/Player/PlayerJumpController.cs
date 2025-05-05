using System;
using RunTime.Data.ValueObjects;
using RunTime.Enums;
using RunTime.Events;
using RunTime.Helpers;
using UnityEngine;

namespace RunTime.Controllers.Player
{
    public class PlayerJumpController : MonoBehaviour
    {
        [SerializeField] private Rigidbody2D rb;
        [SerializeField] private Transform feetTransform;
        
        private JumpState _state = JumpState.Ready;
        
        private PlayerJumpData _data;

        public void OnEnterGround()
        {
            _state = JumpState.Ready;
            // TODO : DoubleJump
        }

        public void SetData(PlayerJumpData data)
        {
            _data = data;
        }

        public void OnJump()
        {
            if(!CheckGround() && (_state != JumpState.Ready || _state != JumpState.CanDoubleJump)) return;

            _state = JumpState.Jumped;
            rb.linearVelocityY = 0;
            rb.AddForce(Caches.Up2 * _data.jumpForce, ForceMode2D.Impulse);
            AnimationEvents.Instance.onTriggerAnimation(AnimationsID.Jump);
        }

        private bool CheckGround()
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
