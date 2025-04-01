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

        animator.SetFloat(AnimationsID.LinearVelocityY, linearVelocityY);

        if(Mathf.Abs(moveInputX) > Mathf.Epsilon)
        {
            moveInputX = Mathf.Sign(moveInputX);
        }

        if(playerFacade.CanMove)
        {
            if(moveInputX != 0)
            {
                playerFacade.Transform.localScale = new Vector3(moveInputX, initialScale.y, initialScale.z);
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
