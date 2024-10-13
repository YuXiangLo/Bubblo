using UnityEngine;

public class Player : MonoBehaviour
{
    private PlayerMovement _playerMovement;
    private PlayerHealth _playerHealth;

    private void Awake()
    {
        // Find the PlayerMovement and PlayerHealth components attached to the player
        _playerMovement = GetComponent<PlayerMovement>();
        _playerHealth = GetComponent<PlayerHealth>();
    }

    private void Update()
    {
        // Delegate the movement handling to PlayerMovement
		if (_playerMovement.enabled)
			_playerMovement.HandleMovement();

        // Test functionality to increase/decrease health with key presses
        TestHealthModification();
    }

    // Additional methods that might serve as a coordinator between health/movement
    public void TakeDamage(float damage)
    {
        // Delegates damage handling to PlayerHealth
        _playerHealth.TakeDamage(damage);
    }

    public void Heal(float amount)
    {
        // Delegates healing to PlayerHealth
        _playerHealth.Heal(amount);
    }

    // Test function to modify health
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

