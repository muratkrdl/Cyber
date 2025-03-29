using UnityEngine;

public class PlayerAnimation : MonoBehaviour
{
    private Animator animator;
    private PlayerFacade playerFacade;
    private Vector3 initialScale;

    public PlayerAnimation(PlayerFacade playerFacade, Animator animator, Vector3 initialScale)
    {
        this.playerFacade = playerFacade;
        this.animator = animator;
        this.initialScale = initialScale;
    }

    public void SetAnimationValues()
    {
        if(GameStateManager.Instance.GetIsGamePaused || playerFacade.OnDash) return;


        float moveInputX = playerFacade.GetPlayerMovementInput().x;
        float linearVelocityY = playerFacade.GetLinearVelocity().y;

        animator.SetFloat(Const.PlayerAnimations.FLOAT_LINEAR_VELOCITY_Y, linearVelocityY);

        if(Mathf.Abs(moveInputX) > Mathf.Epsilon)
        {
            moveInputX = Mathf.Sign(moveInputX);
        }

        if(playerFacade.CanMove)
        {
            if(moveInputX != 0)
            {
                playerFacade.transform.localScale = new(moveInputX, initialScale.y, initialScale.z);
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
