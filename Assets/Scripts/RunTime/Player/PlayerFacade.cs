using Cysharp.Threading.Tasks;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerFacade : GamePlayBehaviour
{
    [Header("References")]
    [SerializeField] private SpriteRenderer playerSpriteRenderer;
    [SerializeField] private Animator animator;

    [Header("Move Settings")]
    [SerializeField] private float moveSpeed = 100;

    [Header("Jump Settings")]
    [SerializeField] private Transform feetTransform;
    [SerializeField] private LayerMask groundLayerMask;
    [SerializeField] private float groundCheckSize = .08f;
    [SerializeField] private float jumpForce = 3f;

    [Header("Attack Settings")]
    [SerializeField] private float comboResetTimer = 3f;
    [SerializeField] private float timeBtwnAttack = .5f;
    [SerializeField] private int maxCombo = 3;

    [Header("Dash Settings")]
    [SerializeField] private float dashSpeed = 4f;
    [SerializeField] private float dashTime = 0.5f;
    [SerializeField] private float spriteAmount = 25f;

    private PlayerInputHandle playerInputHandle;
    private PlayerAnimation playerAnimation;
    private PlayerMovement playerMovement;
    private PlayerJump playerJump;
    private PlayerStateController playerStateController;
    private PlayerAttack playerAttack;
    private PlayerDash playerDash;

    private Rigidbody2D myRigidbody;
    private Transform playerTransform;

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
        playerTransform = transform;

        playerStateController = GetComponent<PlayerStateController>();
        playerInputHandle = GetComponent<PlayerInputHandle>();

        playerMovement = new PlayerMovement(this);
        playerJump = new PlayerJump(this, feetTransform, groundLayerMask, groundCheckSize, jumpForce);
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
            (state is not PlayerIdleState && onDash))
            return;

        playerStateController.SetPlayerState(state);
    }

    // Rigidbody
    public Vector2 GetLinearVelocity() => myRigidbody.linearVelocity;
    public void SetLinearVelocityXToZero() => myRigidbody.linearVelocityX = 0;
    public void SetLinearVelocityYToZero() => myRigidbody.linearVelocityY = 0;
    public void SetGravityScaleToZero() => myRigidbody.gravityScale = 0;
    public void SetGravityScaleToInitial() => myRigidbody.gravityScale = initialGravity;
    public void SetLinearVelocityX(float x) => myRigidbody.linearVelocityX = x;
    public void AddForce(Vector2 direction, ForceMode2D mode = default) => myRigidbody.AddForce(direction, mode);

    // Other References
    public SpriteRenderer GetPlayerSpriteRenderer() => playerSpriteRenderer;
    public Transform Transform => playerTransform;

    // Move
    public PlayerMovement GetPlayerMovement() => playerMovement;
    public Vector2 GetPlayerMovementInput() => playerInputHandle.GetMoveInput;
    public float GetMoveSpeed() => moveSpeed;

    // Jump
    public void PlayerJump() => playerJump.OnJump();
    public bool CheckGround() => playerJump.CheckGround();

    // Attack
    public void OnAttack() => playerAttack.Attack();
    public float GetComboResetTimer() => comboResetTimer;
    public float GetTimeBtwnAttack() => timeBtwnAttack;
    public int GetMaxCombo() => maxCombo;

    // Dash
    public void OnStartDash() => playerDash.StartDash();
    public float GetDashTime() => dashTime;
    public float GetDashSpeed() => dashSpeed;
    public float GetDashSpriteFrequency() => dashTime / spriteAmount;
    
    // Animation
    public void SetAnimationFloat(int floatID, float value) => playerAnimation.SetFloat(floatID, value);
    public void SetAnimationBool(int boolID, bool value) => playerAnimation.SetBool(boolID, value);
    public void SetAnimationTrigger(int triggerID) => playerAnimation.SetTrigger(triggerID);



    private void OnDrawGizmos() 
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(feetTransform.position, groundCheckSize);
    }

    async UniTaskVoid SetCanDashTrue()
    {
        float dashResetTimer = .09f;
        await Extensions.GetUnitaskTime(dashResetTimer);
        canDash = true;
    }

#region GamePlayBehaviour
    protected override void GameStateManager_OnGamePause()
    {
        myRigidbody.gravityScale = 0;
        onPauseLinearVelocityY = myRigidbody.linearVelocityY;
        myRigidbody.linearVelocity = Caches.Zero2;
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
#endregion

}
