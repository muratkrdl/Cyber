using RunTime.Data.UnityObjects;
using RunTime.Events;
using RunTime.Extensions;
using Unity.VisualScripting;
using UnityEngine;

namespace RunTime.Controllers.Player
{
    public class PlayerPhysicsController : MonoBehaviour
    {
        private LayerMask _groundLayerMask;

        private void Awake()
        {
            _groundLayerMask = Resources.Load<CD_Player>("Data/CD_Player").PlayerJumpData.groundLayerMask;
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            
        }

        private void OnCollisionEnter2D(Collision2D other)
        {
            if (_groundLayerMask.LayerMaskContains(other.gameObject.layer))
            {
                PlayerEvents.Instance.onEnterGround?.Invoke();
            }
        }
        
        private void OnCollisionExit2D(Collision2D other)
        {
            if (_groundLayerMask.LayerMaskContains(other.gameObject.layer))
            {
                PlayerEvents.Instance.onExitGround?.Invoke();
            }
        } 
    }
}
