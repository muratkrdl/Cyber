using System.ComponentModel;
using UnityEngine;

public class PlayerAnimation : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private Animator animator;

    private Player player;

    private Vector3 initialScale;

    public Animator GetAnimator
    {
        get => animator;
    }

    private void Awake() 
    {
        initialScale = transform.localScale;

        player = GetComponent<Player>();
    }

    private void Update() 
    {
        if(player.GetGamePaused) return;
        
        SetAnimations();
    }

    private void SetAnimations()
    {
        float moveInputX = player.GetPlayerInputHandle().FramePlayerInput.MoveInput.x;
        float linearVelocityY = player.GetPlayerMovement().GetLinearVelocityY();

        animator.SetFloat(Const.PlayerAnimations.FLOAT_LINEAR_VELOCITY_Y, linearVelocityY);

        if(Mathf.Abs(moveInputX) > Mathf.Epsilon)
        {
            moveInputX = Mathf.Sign(moveInputX);
        }

        if(player.CanMove)
        {
            if(moveInputX != 0)
            {
                transform.localScale = new(moveInputX, initialScale.y, initialScale.z);
            }
            animator.SetFloat(Const.PlayerAnimations.FLOAT_SPEED, Mathf.Abs(moveInputX));
        }
    }

    public void SetTrigger(string triggerName)
    {
        animator.SetTrigger(triggerName);
    }
    public void SetBool(string boolName, bool value)
    {
        animator.SetBool(boolName, value);
    }
    public void SetFloat(string floatName, float value)
    {
        animator.SetFloat(floatName, value);
    }

}
