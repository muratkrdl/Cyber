using UnityEngine;

public class PlayerJump : MonoBehaviour
{
    [Header("Settings")]
    [SerializeField] private Transform feetTransform;
    [SerializeField] private float groundCheckSize;
    [SerializeField] private LayerMask groundLayerMask;
    [SerializeField] private float jumpForce;

    private Player player;

    private void Awake() 
    {
        player = GetComponent<Player>();
    }

    public void OnJump()
    {
        if(!CheckGround()) return;

        player.GetRigidbody.linearVelocityY = 0;
        player.GetRigidbody.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
        player.GetPlayerAnimation().SetTrigger(Const.PlayerAnimations.TRIGGER_JUMP);
    }

    public bool CheckGround()
    {
        Collider2D isGrounded = Physics2D.OverlapCircle(feetTransform.position, groundCheckSize, groundLayerMask);

        if(isGrounded)
        {
            player.GetPlayerAnimation().SetBool(Const.PlayerAnimations.BOOL_ISFALLING, false);
        }
        
        return isGrounded;
    }

    private void OnDrawGizmos() 
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(feetTransform.position, groundCheckSize);
    }

}
