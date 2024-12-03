using UnityEngine;

public class Player : MonoBehaviour, IHealthPercentage, IMagicPercentage, IModifyHealth, IKnockback, IPlayerStatus, IPlayerBubble
{
    private PlayerData PlayerData;
    private Rigidbody2D Rigidbody2D;
    private Camera MainCamera;
    private IMovementState MovementState;
    private IAttackState AttackState;

    #region Animation
    public Animator Animator;
    #endregion

    #region Raycast Information
    private RayCaster MovementRayCaster;
    private RayCaster SlopeRayCaster;
    public bool Grounded => MovementRayCaster.Grounded;
    public bool HitCeiling => MovementRayCaster.HitCeiling;
    public float SlopeAngle => MovementRayCaster.SlopeAngle;
    public CastSide CastSide => MovementRayCaster.CastSide;
    public bool IsSlopeMovement => SlopeRayCaster.SlopeAngle > 0.1f && SlopeRayCaster.SlopeAngle < 60f;
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
    #endregion

    #region Player Health
    private PlayerHealth Health;
    public float HealthPercentage => Health.HealthPercentage;
    public void Heal(float amount) => Health.Heal(amount);
    public void TakeDamage(float amount) => Health.TakeDamage(amount);
    #endregion

    #region MonoBehaviour
    private void Awake()
    {
        PlayerData = GetComponent<PlayerData>();
        Health = GetComponent<PlayerHealth>();
        Magic = GetComponent<PlayerMagic>();
        Animator = GetComponent<Animator>();
        
        Rigidbody2D = GetComponent<Rigidbody2D>();
        Rigidbody2D.freezeRotation = true;

        var casters = GetComponents<RayCaster>();
        MovementRayCaster = casters[0];
        MovementRayCaster.Initialize(Rigidbody2D, Vector2.down, 0.5f * PlayerData.PlayerSize);

        SlopeRayCaster = casters[1];
        SlopeRayCaster.Initialize(Rigidbody2D, Vector2.down, PlayerData.PlayerSize);

        MainCamera = Camera.main;

        MovementState = new MovementInitialState(this, PlayerData);
        AttackState = new AttackIdleState(this, PlayerData);
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
        Debug.Log($"Change Attack State: {newState.GetType().Name}");
        AttackState = newState;
        AttackState.Enter();
    }

    public Bubble InitializeBubble() => Instantiate(PlayerData.BubblePrefab).GetComponent<Bubble>();

    public void Die()
    {
        // TODO: Handle Die logics
    }

    public void Knockback(Vector2 knockbackDirection, float toSleep)
    {
        AttackState.Knockbacked();
        ChangeMovementState(new MovementKnockbackState(this, PlayerData, knockbackDirection, toSleep));
    }

    public void BubbleJump()
    {
        Velocity = new Vector2(Velocity.x, PlayerData.BubbleJumpForce);
        ChangeMovementState(new MovementRisingState(this, PlayerData));
    }

    public void BubbleBurst()
    {
        // TODO: Handle Burst logics
    }

    public void SetAnimation(AnimationStateType state)
    {
        Animator.SetInteger("State", (int)state);
    }

    public void Initialize()
    {
        Health.Initialize();
        Magic.Initialize();
    }

    public PlayerDataStatus GetPlayerStatus()
    {
        return new PlayerDataStatus(Health.HealthPercentage, Magic.MagicPercentage);
    }

    public void SetPlayerStatus(PlayerDataStatus status)
    {
        Health.Initialize(status.Health);
        Magic.Initialize(status.Magic);
    }

    private void PlayerUpdate()
    {
        DetectFaceSide();
        RestrictPlayerWithinCamera();
    }

    private void DetectFaceSide() 
    {
        if (Velocity.x == 0f)
        {
            return;
        }
        Vector3 currentScale = transform.localScale;
        currentScale.x = (Velocity.x > 0f ? 1 : -1) * Mathf.Abs(currentScale.x);
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
}
