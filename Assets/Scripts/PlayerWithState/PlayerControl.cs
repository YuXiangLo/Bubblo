using UnityEngine;

public class PlayerControl : MonoBehaviour, IHealthPercentage, IMagicPercentage, IModifyHealth, IKnockback
{
    public IPlayerMovementState PlayerMovementState;
    public IPlayerAttackState PlayerAttackState;
    
    // Player Movement
    public bool IsGrounded = false;
    public bool IsFacingRight => Velocity.x > 0f;
    public Vector2 Velocity = Vector2.zero;

    // PlayerAttack
    public float CurrentMagicPoint;
    public bool IsHoldingBubble = false;
	public bool IsAttacking = false;
    public float MagicPercentage => CurrentMagicPoint / PlayerData.MaxMagicPoint;
    public float HealthPercentage => PlayerHealth.HealthPercentage; 

    private PlayerData PlayerData;
    private Rigidbody2D Rigidbody2D;
    private PlayerHealth PlayerHealth;
    private Camera MainCamera;
    private Animator Animator;

    public void ChangePlayerMovementState(IPlayerMovementState newPlayerMovementState)
    {
        PlayerMovementState = newPlayerMovementState;
        PlayerMovementState.HandleMovement();
    }

    public void ChangePlayerAttackState(IPlayerAttackState newPlayerAttackState)
    {
        PlayerAttackState = newPlayerAttackState;
        PlayerAttackState.HandleAttack();
    }

    public Bubble InitialBubble()
    {
        return Instantiate(PlayerData.BubblePrefab).GetComponent<Bubble>();
    }

    public void Heal(float amount) 
    {
        PlayerHealth.Heal(amount);
    }

    public void TakeDamage(float amount) 
    {
        PlayerHealth.TakeDamage(amount);
    }

    public void BubbleJump()
    {
        Velocity.y = PlayerData.JumpForce;
    }

    public void Knockback(Vector2 knockbackDirection, float toSleep)
    {
        ChangePlayerMovementState(new PlayerMovementKnockBackState(this, PlayerData, knockbackDirection, toSleep));
	}

    private void Awake()
    {
        PlayerData = GetComponent<PlayerData>();
        PlayerHealth = GetComponent<PlayerHealth>();
        Animator = GetComponent<Animator>();
        Rigidbody2D = GetComponent<Rigidbody2D>();
		Rigidbody2D.freezeRotation = true;
        PlayerMovementState = new PlayerMovementInitialState(this, PlayerData);
        PlayerAttackState = new PlayerAttackInitialState(this, PlayerData);
        MainCamera = Camera.main;
    }

    private void Update()
    {
        DetectPlayerStatus();
        HandleMovement();
        HandleAttack();
        HandleAnimator();
        DetectFaceSide();
        RestrictPlayerWithinCamera();
    }

    private void FixedUpdate()
    {
        Rigidbody2D.MovePosition(Rigidbody2D.position + Velocity * Time.fixedDeltaTime);
    }

    private void DetectPlayerStatus()
    {
        IsGrounded = Rigidbody2D.Raycast(Vector2.down, new Vector2(PlayerData.PlayerSize, PlayerData.PlayerSize), PlayerData.TriggerDistance);
    }

    private void HandleMovement()
    {
        PlayerMovementState.HandleMovement();
    }

    private void HandleAttack()
    {
        PlayerAttackState.HandleAttack();
    }

    private void HandleAnimator()
    {
        Animator.SetFloat("Speed", Mathf.Abs(Velocity.x));
        Animator.SetBool("IsFall", !IsGrounded && Velocity.y < 0);
		Animator.SetBool("IsJump", !IsGrounded && Velocity.y >= 0);
        Animator.SetBool("IsHoldingBubble", IsHoldingBubble);
		Animator.SetBool("IsAttack", IsAttacking);
    }

    private void DetectFaceSide() 
    {
        Vector3 currentScale = transform.localScale;
        if (Velocity.x != 0f)
        {
            currentScale.x = (IsFacingRight ? 1 : -1) * Mathf.Abs(currentScale.x);
        }
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
