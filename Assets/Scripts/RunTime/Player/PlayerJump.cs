using UnityEngine;

public class PlayerJump
{
    private Transform feetTransform;
    private LayerMask groundLayerMask;
    private float groundCheckSize;
    private float jumpForce;

    private PlayerFacade playerFacade;

    public PlayerJump(PlayerFacade playerFacade, Transform feetTransform, LayerMask groundLayerMask, float groundCheckSize, float jumpForce)
    {
        this.playerFacade = playerFacade;
        this.feetTransform = feetTransform;
        this.groundLayerMask = groundLayerMask;
        this.groundCheckSize = groundCheckSize;
        this.jumpForce = jumpForce;
    }

    public void OnJump()
    {
        if(!CheckGround()) return;

        playerFacade.SetLinearVelocityYToZero();
        playerFacade.AddForce(Caches.Up2 * jumpForce, ForceMode2D.Impulse);
        playerFacade.SetAnimationTrigger(AnimationsID.Jump);
    }

    public bool CheckGround()
    {
        Collider2D isGrounded = Physics2D.OverlapCircle(feetTransform.position, groundCheckSize, groundLayerMask);

        if(isGrounded)
        {
            playerFacade.SetAnimationBool(AnimationsID.IsFalling, false);
        }
        
        return isGrounded;
    }

}
