using RunTime.Data.ValueObjects;
using Unity.Mathematics;
using UnityEngine;

namespace RunTime.Controllers.Player
{
    public class PlayerMovementController : MonoBehaviour
    {
        [SerializeField] private Rigidbody2D rb;
        
        private PlayerMovementData _data;
        
        private Vector2 _currentVelocity;

        public void SetData(PlayerMovementData data)
        {
            _data = data;
        }

        public void MoveHandle(float2  input)
        {
            _currentVelocity = rb.linearVelocity;
            _currentVelocity.x = input.x * _data.moveSpeed / 10;
            rb.linearVelocity = _currentVelocity;
        }

        public void StopMove()
        {
            _currentVelocity = rb.linearVelocity;
            _currentVelocity.x = 0;
            rb.linearVelocity = _currentVelocity;
        }
    }
}
