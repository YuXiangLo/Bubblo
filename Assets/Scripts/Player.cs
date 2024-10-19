using UnityEngine;

public class Player : MonoBehaviour, IHealthPercentage, IModifyHealth
{
    private PlayerMovement PlayerMovement;
    private PlayerHealth PlayerHealth;
	private PlayerAttack PlayerAttack;

    public float HealthPercentage { get => PlayerHealth.HealthPercentage; }
	public bool IsFacingRight {get => PlayerMovement.IsFacingRight; }

    public void TakeDamage(float amount)
    {
        // Delegates damage handling to PlayerHealth
        PlayerHealth.TakeDamage(amount);
    }

    public void Heal(float amount)
    {
        // Delegates healing to PlayerHealth
        PlayerHealth.Heal(amount);
    }

    private void Awake()
    {
        // Find the PlayerMovement and PlayerHealth components attached to the player
        PlayerMovement = GetComponent<PlayerMovement>();
        PlayerHealth = GetComponent<PlayerHealth>();
		PlayerAttack = GetComponent<PlayerAttack>();
    }

    private void Update()
    {
        // Delegate the movement handling to PlayerMovement
		if (PlayerMovement.enabled) {
			PlayerMovement.HandleMovement();
			PlayerAttack.HandleAttack();
		}
        // Test functionality to increase/decrease health with key presses
        TestHealthModification();
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

