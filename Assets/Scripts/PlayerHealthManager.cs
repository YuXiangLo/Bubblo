using UnityEngine;
using UnityEngine.UI;

public class PlayerHealthManager : MonoBehaviour
{
    public Image HealthBar;  // Reference to the UI health bar image
    public PlayerHealth PlayerHealth;  // Reference to the PlayerHealth component

    private void Update()
    {
        // Update the health bar to reflect the current health
        UpdateHealthBar();
    }

    /// <summary>
    /// Method to update the health bar fill amount
    /// </summary>
    private void UpdateHealthBar()
    {
        // Get the current health percentage from PlayerHealth
        float healthPercentage = PlayerHealth.GetCurrentHealthPercentage();
        HealthBar.fillAmount = healthPercentage;
    }
}

