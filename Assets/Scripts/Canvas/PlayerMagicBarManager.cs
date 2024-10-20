using UnityEngine;
using UnityEngine.UI;

public class PlayerMagicBarManager : MonoBehaviour
{
    private Image MagicFill;  // Reference to the UI health bar image
    private IMagicPercentage Player;  // Reference to the PlayerHealth component

    private void Start()
    {
        MagicFill = transform.Find("Fill").GetComponent<Image>();
        Player = FindObjectOfType<Player>().GetComponent<IMagicPercentage>();
    }

    private void Update()
    {
        // Update the health bar to reflect the current health
        MagicFill.fillAmount = Player.MagicPercentage;
    }
}