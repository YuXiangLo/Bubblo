using UnityEngine;

public class Player : MonoBehaviour, IHealthPercentage, IMagicPercentage, IModifyHealth, IKnockback {
    private PlayerMovement PlayerMovement;
    private PlayerHealth PlayerHealth;
	private PlayerAttack PlayerAttack;
    public Animator animator;

    public float HealthPercentage { get => PlayerHealth.HealthPercentage; }
    public float MagicPercentage { get => PlayerAttack.MagicPercentage; }
	public bool IsFacingRight { get => PlayerMovement.IsFacingRight; }
	public bool IsGrounded { get => PlayerMovement.IsGrounded; }
    public bool IsBubbleHeld { get => PlayerAttack.IsButtonHeld; }

    public void TakeDamage(float amount) {
        PlayerHealth.TakeDamage(amount);
    }

	public void Knockback(Vector2 knockbackDirection, float toSleep) {
		PlayerMovement.Knockback(knockbackDirection, toSleep);
	}

    public void Heal(float amount) {
        PlayerHealth.Heal(amount);
    }

	public void BubbleJump() {
		PlayerMovement.BubbleJump();
	}

    private void Awake() {
        PlayerMovement = GetComponent<PlayerMovement>();
        PlayerHealth = GetComponent<PlayerHealth>();
		PlayerAttack = GetComponent<PlayerAttack>();
    }

    private void Update() {
		if (PlayerMovement.enabled) {
			PlayerMovement.HandleMovement();
			PlayerAttack.HandleAttack();
		}
        TestHealthModification();
        animator.SetFloat("speed", PlayerMovement.Speed);
        animator.SetBool("isFloating", !IsGrounded);
        animator.SetBool("isHoldingBubble", IsBubbleHeld);
    }

    /// <summary>
    /// Test method to simulate health modification
    /// </summary>
    private void TestHealthModification()
    {
        // Press "Q" to reduce health (simulate damage)
        if (Input.GetKeyDown(KeyCode.Q))
        {
            TakeDamage(10f);  // Reduces health by 10
            Debug.Log("Player took 10 damage");
        }

        // Press "E" to increase health (simulate healing)
        if (Input.GetKeyDown(KeyCode.E))
        {
            Heal(10f);  // Increases health by 10
            Debug.Log("Player healed 10 health");
        }
    }
}

