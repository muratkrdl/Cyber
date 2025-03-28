using UnityEngine;

public class Player : GamePlayBehaviour
{
    [Header("Common References")]
    [SerializeField] private Rigidbody2D myRigidbody;

    private PlayerInputHandle playerInputHandle;
    private PlayerAnimation playerAnimation;
    private PlayerMovement playerMovement;
    private PlayerJump playerJump;
    private PlayerStateController playerStateController;
    private PlayerAttack playerAttack;
    private PlayerEvents playerEvents;

    private bool canMove = true;
    private float initialGravity;
    private float onPauseLinearVelocityY;

    public bool CanMove
    {
        get => canMove;
        set => canMove = value;
    }

    public Rigidbody2D GetRigidbody
    {
        get => myRigidbody;
    }

    private void Awake() 
    {
        playerInputHandle = GetComponent<PlayerInputHandle>();
        playerAnimation = GetComponent<PlayerAnimation>();
        playerMovement = GetComponent<PlayerMovement>();
        playerJump = GetComponent<PlayerJump>();
        playerStateController = GetComponent<PlayerStateController>();
        playerAttack = GetComponent<PlayerAttack>();
        playerEvents = GetComponent<PlayerEvents>();

        initialGravity = GetRigidbody.gravityScale;
    }

    protected override void Start()
    {
        base.Start();
        GetPlayerEvents().OnStateChange += PlayerEvents_OnStateChange;
    }

    private void PlayerEvents_OnStateChange(IPlayerState state)
    {
        if(GetGamePaused) return;

        GetPlayerStateController().SetPlayerState(state);
    }

    public PlayerInputHandle GetPlayerInputHandle()
    {
        return playerInputHandle;
    }
    public PlayerAnimation GetPlayerAnimation()
    {
        return playerAnimation;
    }
    public PlayerMovement GetPlayerMovement()
    {
        return playerMovement;
    }
    public PlayerJump GetPlayerJump()
    {
        return playerJump;
    }
    public PlayerStateController GetPlayerStateController()
    {
        return playerStateController;
    }
    public PlayerAttack GetPlayerAttack()
    {
        return playerAttack;
    }
    public PlayerEvents GetPlayerEvents()
    {
        return playerEvents;
    }

    public float GetHorizontalMovementInput()
    {
        return GetPlayerInputHandle().FramePlayerInput.MoveInput.x;
    }

    protected override void GameStateManager_OnGamePause()
    {
        GetRigidbody.gravityScale = 0;
        onPauseLinearVelocityY = GetRigidbody.linearVelocityY;
        GetRigidbody.linearVelocity = Vector2.zero;
        GetPlayerAnimation().GetAnimator.speed = 0;
        GetPlayerEvents().OnStateChange?.Invoke(new PlayerPauseState());
        base.GameStateManager_OnGamePause();
    }

    protected override void GameStateManager_OnGameResume()
    {
        base.GameStateManager_OnGameResume();
        GetRigidbody.gravityScale = initialGravity;
        GetRigidbody.linearVelocityY = onPauseLinearVelocityY;
        GetPlayerAnimation().GetAnimator.speed = 1;
        GetPlayerEvents().OnStateChange?.Invoke(new PlayerIdleState());
    }

    protected override void OnDestroy() 
    {
        base.OnDestroy();
        GetPlayerEvents().OnStateChange -= PlayerEvents_OnStateChange;
    }

}
