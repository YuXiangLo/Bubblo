using UnityEngine;
using UnityEngine.UI;

public class HealthManager : MonoBehaviour
{
    public Image healthBar;  // Reference to the UI health bar image
    public PlayerHealth playerHealth;  // Reference to the PlayerHealth component

    private void Update()
    {
        // Update the health bar to reflect the current health
        UpdateHealthBar();
    }

    // Method to update the health bar fill amount
    private void UpdateHealthBar()
    {
        // Get the current health percentage from PlayerHealth
        float healthPercentage = playerHealth.GetCurrentHealthPercentage();
        healthBar.fillAmount = healthPercentage;
    }
}

