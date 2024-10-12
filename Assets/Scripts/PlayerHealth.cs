using UnityEngine;
using System;

public class PlayerHealth : MonoBehaviour
{
    public float maxHealth = 100f;
    public float currentHealth;

    // Event to notify other systems (like the UI) about health changes
    public event Action<float> OnHealthChanged;

    private void Start()
    {
        // Initialize health at the beginning of the game
        currentHealth = maxHealth;
        OnHealthChanged?.Invoke(currentHealth / maxHealth);  // Notify UI at start
    }

	public float GetCurrentHealthPercentage() {
		return currentHealth / maxHealth;
	}

    public void TakeDamage(float damage)
    {
        // Decrease the player's health
        currentHealth -= damage;
        currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth);
        OnHealthChanged?.Invoke(currentHealth / maxHealth);  // Notify about the health change

        if (currentHealth <= 0)
        {
            Die();  // Trigger death if health is zero
        }
    }

    public void Heal(float amount)
    {
        // Increase the player's health
        currentHealth += amount;
        currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth);
        OnHealthChanged?.Invoke(currentHealth / maxHealth);  // Notify about the health change
    }

    private void Die()
    {
        // Handle player death (e.g., disable movement, show game over screen, etc.)
        Debug.Log("Player has died!");
    }
}

