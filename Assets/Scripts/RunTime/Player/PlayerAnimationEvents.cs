using UnityEngine;

public class PlayerAnimationEvents : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private Player player;

    public void AnimEvent_StartFalling()
    {
        player.GetPlayerAnimation().SetBool(Const.PlayerAnimations.BOOL_ISFALLING, true);
    }

    public void AnimEvent_StopFalling()
    {
        player.GetPlayerAnimation().SetBool(Const.PlayerAnimations.BOOL_ISFALLING, false);
    }

    public void AnimEvent_Attack()
    {
        
    }

    public void AnimEvent_OnTouchGround()
    {
        if(player.GetPlayerJump().CheckGround())
        {
            player.CanDash = true;
        }
    }

}
