using RunTime.Data.ValueObjects;
using Unity.Mathematics;
using UnityEngine;

namespace RunTime.Controllers.Player
{
    public class PlayerMovementController : MonoBehaviour
    {
        [SerializeField] private Rigidbody2D rb;
        
        private PlayerMovementData _data;

        private float velocityX;

        public void SetData(PlayerMovementData data)
        {
            _data = data;
        }

        public void MoveHandle(float2  input)
        {
            velocityX = input.x * _data.moveSpeed * Time.fixedDeltaTime;
            rb.linearVelocityX = velocityX;
        }

        public void StopMove()
        {
            rb.linearVelocity = Vector2.zero;
        }
    }
}
