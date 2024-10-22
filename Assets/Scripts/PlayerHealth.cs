using UnityEngine;
using UnityEngine.Events;

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
            GetComponent<Player>().Die();
        }
    }

    public void Heal(float amount)
    {
        // Increase the player's health
        CurrentHealth += amount;
        CurrentHealth = Mathf.Clamp(CurrentHealth, 0, MaxHealth);
        OnHealthChanged.Invoke(CurrentHealth / MaxHealth);  // Notify about the health change
    }
}

