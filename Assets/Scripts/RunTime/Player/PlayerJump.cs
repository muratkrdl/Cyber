using UnityEngine;

public class PlayerJump : MonoBehaviour
{
    [Header("Settings")]
    [SerializeField] private Transform feetTransform;
    [SerializeField] private float groundCheckSize;
    [SerializeField] private LayerMask groundLayerMask;
    [SerializeField] private float jumpForce;

    private PlayerFacade playerFacade;

    private void Awake() 
    {
        playerFacade = GetComponent<PlayerFacade>();
    }

    public void OnJump()
    {
        if(!CheckGround()) return;

        playerFacade.SetLinearVelocityYToZero();
        playerFacade.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
        playerFacade.SetAnimationTrigger(Const.PlayerAnimations.TRIGGER_JUMP);
    }

    public bool CheckGround()
    {
        Collider2D isGrounded = Physics2D.OverlapCircle(feetTransform.position, groundCheckSize, groundLayerMask);

        if(isGrounded)
        {
            playerFacade.SetAnimationBool(Const.PlayerAnimations.BOOL_ISFALLING, false);
        }
        
        return isGrounded;
    }

    private void OnDrawGizmos() 
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(feetTransform.position, groundCheckSize);
    }

}
