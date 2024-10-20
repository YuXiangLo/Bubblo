using UnityEngine;
using UnityEngine.UI;

public class PlayerHealthBarManager : MonoBehaviour
{
    private Image HealthFill;  // Reference to the UI health bar image
    private IHealthPercentage Player;  // Reference to the PlayerHealth component

    private void Start()
    {
        HealthFill = transform.Find("Fill").GetComponent<Image>();
        Player = FindObjectOfType<Player>().GetComponent<IHealthPercentage>();
    }

    private void Update()
    {
        // Update the health bar to reflect the current health
        HealthFill.fillAmount = Player.HealthPercentage;
    }
}

