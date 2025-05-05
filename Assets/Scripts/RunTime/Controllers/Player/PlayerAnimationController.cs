using RunTime.Events;
using RunTime.Helpers;
using RunTime.Keys;
using Unity.Mathematics;
using UnityEngine;

namespace RunTime.Controllers.Player
{
    public class PlayerAnimationController : MonoBehaviour
    {
        [SerializeField] private Animator animator;
        [SerializeField] private Rigidbody2D rb;
        
        private float3 _initialScale;
        private float _linearVelocityY;
        private float _moveInputX;
        
        private void Awake()
        {
            _initialScale = transform.localScale;
        }

        private void OnEnable()
        {
            SubscribeEvents();
        }

        private void SubscribeEvents()
        {
            InputEvents.Instance.onStartMove += OnStartMove;
            InputEvents.Instance.onStopMove += OnStopMove;

            AnimationEvents.Instance.onTriggerAnimation += OnTriggerAnimation;
            AnimationEvents.Instance.onBoolAnimation += OnBoolAnimation;
            AnimationEvents.Instance.onFloatAnimation += OnFloatAnimation;
        }
        
        private void OnStartMove(float2 value)
        {
            _moveInputX = value.x;
        }
        
        private void OnStopMove()
        {
            _moveInputX = 0;
        }
        
        private void OnTriggerAnimation(int triggerID)
        {
            animator.SetTrigger(triggerID);
        }
        private void OnBoolAnimation(AnimationBoolParams param)
        {
            animator.SetBool(param.Id, param.Value);
        }
        private void OnFloatAnimation(AnimationFloatParams param)
        {
            animator.SetFloat(param.Id, param.Value);
        }
        
        private void UnSubscribeEvents()
        {
            InputEvents.Instance.onStartMove -= OnStartMove;
            InputEvents.Instance.onStopMove -= OnStopMove;
        }

        private void OnDisable()
        {
            UnSubscribeEvents();
        }

        private void LateUpdate()
        {
            SetAnimationValues();
        }

        private void SetAnimationValues()
        {
            _linearVelocityY = rb.linearVelocityY;
            
            if(Mathf.Abs(_moveInputX) > Mathf.Epsilon)
            {
                _moveInputX = Mathf.Sign(_moveInputX);
                transform.localScale = new Vector3(_moveInputX, _initialScale.y, _initialScale.z);
            }

            OnFloatAnimation(new AnimationFloatParams() { Id = AnimationsID.LinearVelocityY, Value = _linearVelocityY });
            OnFloatAnimation(new AnimationFloatParams() { Id = AnimationsID.Speed, Value = Mathf.Abs(_moveInputX) });
        }
        
    }
}
