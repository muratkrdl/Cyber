using Cysharp.Threading.Tasks;
using UnityEngine;

public class PlayerFacade : GamePlayBehaviour
{
    [Header("References")]
    [SerializeField] private SpriteRenderer playerSpriteRenderer;
    [SerializeField] private Animator animator;

    [Header("Move Settings")]
    [SerializeField] private float moveSpeed = 100;

    [Header("Attack Settings")]
    [SerializeField] private float comboResetTimer;
    [SerializeField] private float timeBtwnAttack;
    [SerializeField] private int maxCombo;

    [Header("Dash Settings")]
    [SerializeField] private float dashSpeed = 4;
    [SerializeField] private float dashTime = 0.5f;
    [SerializeField] private float spriteAmount = 25;

    private PlayerInputHandle playerInputHandle;
    private PlayerAnimation playerAnimation;
    private PlayerMovement playerMovement;
    private PlayerJump playerJump;
    private PlayerStateController playerStateController;
    private PlayerAttack playerAttack;
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
            if(value && !canDash)
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

    private void Awake()
    {
        myRigidbody = GetComponent<Rigidbody2D>();

        playerStateController = GetComponent<PlayerStateController>();
        playerInputHandle = GetComponent<PlayerInputHandle>();
        
        playerJump = GetComponent<PlayerJump>();

        playerMovement = new PlayerMovement(this);
        playerAttack = new PlayerAttack(this);
        playerAnimation = new (this, animator, transform.localScale);
        playerDash = new PlayerDash(this);

        initialGravity = myRigidbody.gravityScale;
    }

    protected override void Start()
    {
        base.Start();
        PlayerEvents.Instance.OnStateChange += PlayerEvents_OnStateChange;
    }

    private void Update() 
    {
        playerAnimation.SetAnimationValues();
    }

    private void PlayerEvents_OnStateChange(IPlayerState state)
    {
        if(
            GameStateManager.Instance.GetIsGamePaused ||
            (state is PlayerDashState && !canDash) ||
            (state is PlayerJumpState && onDash)) 
            return;

        playerStateController.SetPlayerState(state);
    }

    public float GetMoveSpeed() => moveSpeed;

    public float GetDashTime() => dashTime;
    public float GetDashSpeed() => dashSpeed;
    public float GetDashSpriteFrequency() => dashTime / spriteAmount;

    public float GetComboResetTimer() => comboResetTimer;
    public float GetTimeBtwnAttack() => timeBtwnAttack;
    public int GetMaxCombo() => maxCombo;

    public SpriteRenderer GetPlayerSpriteRenderer() => playerSpriteRenderer;
    

    public void SetAnimationFloat(string floatName, float value) => playerAnimation.SetFloat(floatName, value);
    public void SetAnimationBool(string boolName, bool value) => playerAnimation.SetBool(boolName, value);
    public void SetAnimationTrigger(string triggerName) => playerAnimation.SetTrigger(triggerName);

    public bool CheckGround() => playerJump.CheckGround();
    public void PlayerJump() => playerJump.OnJump();

    public Vector2 GetPlayerMovementInput() => playerInputHandle.GetMoveInput;
    public PlayerMovement GetPlayerMovement() => playerMovement;

    public void OnAttack() => playerAttack.OnAttack();

    public void StartDash() => playerDash.StartDash();

    public Vector2 GetLinearVelocity() => myRigidbody.linearVelocity;
    public void SetLinearVelocityXToZero() => myRigidbody.linearVelocityX = 0;
    public void SetLinearVelocityYToZero() => myRigidbody.linearVelocityY = 0;
    public void SetGravityScaleToZero() => myRigidbody.gravityScale = 0;
    public void SetGravityScaleToInitial() => myRigidbody.gravityScale = initialGravity;
    public void SetLinearVelocityX(float x) => myRigidbody.linearVelocityX = x;
    public void AddForce(Vector2 direction, ForceMode2D mode = default) => myRigidbody.AddForce(direction, mode);




    protected override void GameStateManager_OnGamePause()
    {
        myRigidbody.gravityScale = 0;
        onPauseLinearVelocityY = myRigidbody.linearVelocityY;
        myRigidbody.linearVelocity = Vector2.zero;
        animator.speed = 0;
        PlayerEvents.Instance.OnStateChange?.Invoke(new PlayerPauseState());
    }

    protected override void GameStateManager_OnGameResume()
    {
        myRigidbody.gravityScale = initialGravity;
        myRigidbody.linearVelocityY = onPauseLinearVelocityY;
        animator.speed = 1;
        PlayerEvents.Instance.OnStateChange?.Invoke(new PlayerIdleState());
    }

    protected override void OnDestroy()
    {
        base.OnDestroy();
        PlayerEvents.Instance.OnStateChange -= PlayerEvents_OnStateChange;
    }

    async UniTaskVoid SetCanDashTrue()
    {
        await Extensions.GetUnitaskTime(.1f);
        canDash = true;
    }

}
