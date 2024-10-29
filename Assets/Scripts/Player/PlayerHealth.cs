using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    public float MaxHealth = 100f;
    public float CurrentHealth;

    public float HealthPercentage { get => CurrentHealth / MaxHealth; }

    private void Start()
    {
        // Initialize health at the beginning of the game
        CurrentHealth = MaxHealth;
    }

    public void TakeDamage(float damage)
    {
        // Decrease the player's health
        CurrentHealth -= damage;
        CurrentHealth = Mathf.Clamp(CurrentHealth, 0, MaxHealth);

        if (CurrentHealth <= 0)
        {
            Die();  // Trigger death if health is zero
        }
    }

    public void Heal(float amount)
    {
        // Increase the player's health
        CurrentHealth += amount;
        CurrentHealth = Mathf.Clamp(CurrentHealth, 0, MaxHealth);
    }

    private void Die()
    {
        Debug.Log("Player has died!");
    }
}

