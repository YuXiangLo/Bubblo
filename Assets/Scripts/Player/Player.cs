using UnityEngine;
using System.Collections;
using System;


public class Player : MonoBehaviour, IHealthPercentage, IMagicPercentage, IModifyHealth, IKnockback, IPlayerBubble, IRescuedCount
{
    private PlayerData PlayerData;
    private Rigidbody2D Rigidbody2D;
    private IMovementState MovementState;
    private IAttackState AttackState;
    public bool AttackEnabled => MovementState.AttackEnabled;

    #region Camera
    private Camera MainCamera;
    private CameraFollow CameraFollow;
    public bool CameraEndLevel
    {
        get => CameraFollow.EndLevel;
        set => CameraFollow.EndLevel = value;
    }
    #endregion

    #region Animation
    public Animator Animator;
    #endregion

    #region Raycast Information
    private BodyCaster BodyCaster;
    private SlopeTransitionCaster SlopeCaster;
    private LadderCaster LadderCaster;
    private InteractCaster InteractCaster;
    public bool Grounded => BodyCaster.Grounded;
    public bool HitCeiling => BodyCaster.HitCeiling;
    public float SlopeAngle => BodyCaster.SlopeAngle;
    public CastSide CastSide => BodyCaster.CastSide;
    public bool IsSlopeMovement => SlopeCaster.IsSlopeMovement;
    public bool IsAbleToClimb => LadderCaster.IsAbleToClimb;
    public Action Interaction => InteractCaster.Interaction;
    #endregion

    #region Player Movement
    public bool IsFacingRight => transform.localScale.x > 0f;
    public Vector2 Velocity { get; set; } = Vector2.zero;
    #endregion

    #region Player Magic
    private PlayerMagic Magic;
    public float MagicPercentage => Magic.MagicPercentage;
    public bool IsMagicEmpty => Magic.IsEmpty;
    public void Consume(float amount) => Magic.Consume(amount);
    public void Recharge(float amount) => Magic.Recharge(amount);
    public Bubble HoldingBubble { get; set; }
    #endregion

    #region Player Health
    private PlayerHealth Health;
    public bool IsDead = false;
    public float HealthPercentage => Health.HealthPercentage;
    public void Heal(float amount) => Health.Heal(amount);
    public void TakeDamage(float amount) => Health.TakeDamage(amount);
    #endregion

    #region Rescue
    public int RescuedCount { get; set; }
    #endregion

	#region IsGameOver
	private bool IsGameOver = false;
	public GameObject GameOverPanel;
	#endregion

    #region MonoBehaviour
    private void Awake()
    {
        PlayerData = GetComponent<PlayerData>();
        Health = GetComponent<PlayerHealth>();
        Magic = GetComponent<PlayerMagic>();

        Initialize();
        
        Animator = GetComponent<Animator>();
        
        Rigidbody2D = GetComponent<Rigidbody2D>();
        Rigidbody2D.freezeRotation = true;

        BodyCaster = GetComponent<BodyCaster>();
        BodyCaster.Initialize(Rigidbody2D, Vector2.down, 0.5f * PlayerData.PlayerSize);

        SlopeCaster = GetComponent<SlopeTransitionCaster>();
        SlopeCaster.Initialize(Rigidbody2D, Vector2.down, PlayerData.PlayerSize);

        LadderCaster = GetComponent<LadderCaster>();
        LadderCaster.Initialize(Rigidbody2D, Vector2.up, 0.1f * PlayerData.PlayerSize, PlayerData.PlayerSize);

        InteractCaster = GetComponent<InteractCaster>();
        InteractCaster.Initialize(Rigidbody2D, Vector2.down, 0.5f * PlayerData.PlayerSize);

        MainCamera = Camera.main;
        CameraFollow = MainCamera.GetComponent<CameraFollow>();

        AttackState = new AttackIdleState(this, PlayerData);
        MovementState = new MovementEnterLevelState(this, PlayerData);
    }

    private void Update()
    {
        MovementState.Update();
        AttackState.Update();
        PlayerUpdate();
    }

    private void FixedUpdate()
    {
        Rigidbody2D.MovePosition(Rigidbody2D.position + Velocity * Time.fixedDeltaTime);
    }
    #endregion

    public void ChangeMovementState(IMovementState newState)
    {
        MovementState = newState;
        MovementState.Enter();
    }

    public void ChangeAttackState(IAttackState newState)
    {
        AttackState = newState;
        AttackState.Enter();
    }

    public Bubble InitializeBubble() => Instantiate(PlayerData.BubblePrefab).GetComponent<Bubble>();

    public void Die()
    {
        if (IsDead)
        {
            return;
        }
        IsDead = true;
        AttackState.Knockbacked();
        ChangeMovementState(new MovementDieState(this, PlayerData));
        ChangeAttackState(new AttackIdleState(this, PlayerData));
    }

    public void Knockback(Vector2 knockbackDirection, float toSleep)
    {
        if (IsDead)
        {
            return;
        }
        AttackState.Knockbacked();
        ChangeMovementState(new MovementKnockbackState(this, PlayerData, knockbackDirection, toSleep));
    }

    public void BubbleJump()
    {
        float VelocityY = Mathf.Max(PlayerData.BubbleJumpForce, Velocity.y + PlayerData.BubbleJumpForce);
        Velocity = new Vector2(Velocity.x, VelocityY);
        ChangeMovementState(new MovementRisingState(this, PlayerData));
    }

    public void BubbleBurst()
    {
        ChangeAttackState(new AttackIdleState(this, PlayerData));
		ChangeMovementState(new MovementInitialState(this, PlayerData));
    }

    public void Rescue()
    {
        ChangeAttackState(new AttackIdleState(this, PlayerData));
        ChangeMovementState(new MovementRescueState(this, PlayerData));
		RescuedCount++;
    }

    public void EndLevel()
    {
        ChangeAttackState(new AttackIdleState(this, PlayerData));
        ChangeMovementState(new MovementAchieveState(this, PlayerData));
		IsGameOver = true;
    }

    public void SetAnimation(AnimationStateType nextState)
    {
        bool isAttackAnimation = nextState == AnimationStateType.Pitching || nextState == AnimationStateType.Charging;
        if (isAttackAnimation)
        {
            Animator.SetInteger("State", (int)nextState);
        }
        else if (AttackState == null || !AttackState.LockAnimation)
        {
            Animator.SetInteger("State", (int)nextState);
        }
    }

    private void Initialize()
    {
        Health.Initialize();
        Magic.Initialize();
        RescuedCount = 0;
    }

    private void PlayerUpdate()
    {
#if UNITY_EDITOR
        // Add test code here
        if (Input.GetKeyDown(KeyCode.Slash))
        {
            Debug.Log("Rescue test");
            ChangeMovementState(new MovementRescueState(this, PlayerData));
        }
        if (Input.GetKeyDown(KeyCode.Period))
        {
            Debug.Log("Achieve test");
            ChangeMovementState(new MovementAchieveState(this, PlayerData));
        }
#endif
        DetectFaceSide();
		if (!IsGameOver)
			RestrictPlayerWithinCamera();
        DetectInteraction();
    }

    private void DetectFaceSide() 
    {
        if (Velocity.x == 0f)
        {
            return;
        }
        Vector3 currentScale = transform.localScale;
        var isKnockbacked = MovementState is MovementKnockbackState;
        var isFacingRight = (Velocity.x > 0f && !isKnockbacked) || (Velocity.x < 0f && isKnockbacked);
        currentScale.x = (isFacingRight ? 1 : -1) * Mathf.Abs(currentScale.x);
        transform.localScale = currentScale;
    }

    private void RestrictPlayerWithinCamera() 
    {
        float cameraHalfWidth = MainCamera.orthographicSize * MainCamera.aspect;
        float cameraLeftEdge = MainCamera.transform.position.x - cameraHalfWidth;
        float cameraRightEdge = MainCamera.transform.position.x + cameraHalfWidth;

        Vector2 playerPosition = transform.position;
        playerPosition.x = Mathf.Clamp(playerPosition.x, cameraLeftEdge + PlayerData.PlayerSize / 2, cameraRightEdge - PlayerData.PlayerSize / 2);
        transform.position = playerPosition;
    }

    private void DetectInteraction()
    {
        if (UserInput.Instance.InteractKeyDown)
        {
            if (Interaction != null)
            {
                Interaction.Invoke();
            }
        }
    }
}
