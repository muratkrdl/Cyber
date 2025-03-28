using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [Header("Settings")]
    [SerializeField] private float moveSpeed;

    private Player player;

    private float velocityX;

    private void Awake() 
    {
        player = GetComponent<Player>();
    }

    public void MoveHandle()
    {
        float horizontaMovement = player.GetPlayerInputHandle().FramePlayerInput.MoveInput.x;
        velocityX = horizontaMovement * moveSpeed * Time.fixedDeltaTime;
        player.GetRigidbody.linearVelocityX = velocityX;
    }

    public void SetLinearVelocityXToZero()
    {
        velocityX = 0;
        player.GetRigidbody.linearVelocityX = velocityX;
    }

    public float GetLinearVelocityY()
    {
        return player.GetRigidbody.linearVelocityY;
    }

}
