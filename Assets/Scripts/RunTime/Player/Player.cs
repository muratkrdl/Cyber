using Cysharp.Threading.Tasks;
using UnityEngine;

public class Player : GamePlayBehaviour
{
    [Header("Common References")]
    [SerializeField] private SpriteRenderer playerSpriteRenderer;

    private PlayerInputHandle playerInputHandle;
    private PlayerAnimation playerAnimation;
    private PlayerMovement playerMovement;
    private PlayerJump playerJump;
    private PlayerStateController playerStateController;
    private PlayerAttack playerAttack;
    private PlayerEvents playerEvents;
    private PlayerDash playerDash;

    private Rigidbody2D myRigidbody;

    private bool canMove = true;
    private bool canDash;
    private bool onDash;
    private float initialGravity;
    private float onPauseLinearVelocityY;

    public bool CanMove
    {
        get => canMove;
        set => canMove = value;
    }
    public bool CanDash
    {
        get => canDash;
        set
        {
            if(value)
            {
                SetCanDashTrue().Forget();
            }
            else
            {
                canDash = value;
            }
        }
    }
    public bool OnDash
    {
        get => onDash;
        set => onDash = value;
    }

    public float GetInitialGravity
    {
        get => initialGravity;
    }

    public Rigidbody2D GetRigidbody
    {
        get => myRigidbody;
    }
    public SpriteRenderer GetPlayerSpriteRenderer
    {
        get => playerSpriteRenderer;
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
        playerDash = GetComponent<PlayerDash>();

        myRigidbody = GetComponent<Rigidbody2D>();

        initialGravity = GetRigidbody.gravityScale;
    }

    protected override void Start()
    {
        base.Start();
        GetPlayerEvents().OnStateChange += PlayerEvents_OnStateChange;
    }

    private void PlayerEvents_OnStateChange(IPlayerState state)
    {
        if(
            GameStateManager.Instance.GetIsGamePaused ||
            (state is PlayerDashState && !canDash) ||
            (state is PlayerJumpState && onDash)) 
            return;

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
    public PlayerDash GetPlayerDash()
    {
        return playerDash;
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
    }

    protected override void GameStateManager_OnGameResume()
    {
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

    async UniTaskVoid SetCanDashTrue()
    {
        await Extensions.GetUnitaskTime(.1f);
        canDash = true;
    }

}
