using UnityEngine;

public class PlayerAnimationEvents : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private PlayerFacade player;

    public void AnimEvent_StartFalling()
    {
        player.SetAnimationBool(Const.PlayerAnimations.BOOL_ISFALLING, true);
    }

    public void AnimEvent_StopFalling()
    {
        player.SetAnimationBool(Const.PlayerAnimations.BOOL_ISFALLING, false);
    }

    public void AnimEvent_Attack()
    {
        
    }

    public void AnimEvent_OnTouchGround()
    {
        if(player.CheckGround())
        {
            player.CanDash = true;
        }
    }

}
