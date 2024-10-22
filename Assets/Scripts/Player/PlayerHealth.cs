using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class PlayerHealth : MonoBehaviour
{
    public float MaxHealth = 100f;
    public float CurrentHealth;

    public float HealthPercentage { get => CurrentHealth / MaxHealth; }

    // Event to notify other systems (like the UI) about health changes
    public UnityEvent<float> OnHealthChanged;

    private void Start()
    {
        // Initialize health at the beginning of the game
        CurrentHealth = MaxHealth;
        OnHealthChanged.Invoke(CurrentHealth / MaxHealth);  // Notify UI at start
    }

    public void TakeDamage(float damage)
    {
        // Decrease the player's health
        CurrentHealth -= damage;
        CurrentHealth = Mathf.Clamp(CurrentHealth, 0, MaxHealth);
        OnHealthChanged?.Invoke(CurrentHealth / MaxHealth);  // Notify about the health change

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
        OnHealthChanged.Invoke(CurrentHealth / MaxHealth);  // Notify about the health change
    }

    private void Die()
    {
        // Handle player death (e.g., disable movement, show game over screen, etc.)
        SceneManager.LoadScene("Start");
        Debug.Log("Player has died!");
    }
}

