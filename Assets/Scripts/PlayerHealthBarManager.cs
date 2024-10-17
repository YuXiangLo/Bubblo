using UnityEngine;
using UnityEngine.UI;

public class PlayerHealthBarManager : MonoBehaviour
{
    private Image HealthBar;  // Reference to the UI health bar image
    private IHealthPercentage Health;  // Reference to the PlayerHealth component

    private void Start()
    {
        HealthBar = transform.Find("Green").GetComponent<Image>();
        Health = FindObjectOfType<Player>().GetComponent<IHealthPercentage>();
    }

    private void Update()
    {
        // Update the health bar to reflect the current health
        HealthBar.fillAmount = Health.HealthPercentage;
    }
}

