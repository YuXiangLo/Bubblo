using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    public float MaxHealth = 100f;
    public Player Player;

    private void Start()
    {
        Player = GetComponent<Player>();
        // Initialize health at the beginning of the game
        Player.CurrentHealth = MaxHealth;
    }

    public void TakeDamage(float damage)
    {
        // Decrease the player's health
        Player.CurrentHealth -= damage;
        Player.CurrentHealth = Mathf.Clamp(Player.CurrentHealth, 0, MaxHealth);

        if (Player.CurrentHealth <= 0)
        {
            Die();  // Trigger death if health is zero
        }
    }

    public void Heal(float amount)
    {
        // Increase the player's health
        Player.CurrentHealth += amount;
        Player.CurrentHealth = Mathf.Clamp(Player.CurrentHealth, 0, MaxHealth);
    }

    private void Die()
    {
        Debug.Log("Player has died!");
    }
}

