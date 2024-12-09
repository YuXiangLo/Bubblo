using UnityEngine;
using TMPro;

public class DisplayCounts : MonoBehaviour
{
    public TextMeshProUGUI displayText; // Assign this in the Inspector

    void Update()
    {
        // Safely retrieve the Player's RescueCount
        Player player = FindObjectOfType<Player>();
        int rescuedCount = 0; // Default value in case Player is not found
        if (player != null && player.GetComponent<IRescuedCount>() != null)
        {
            rescuedCount = player.GetComponent<IRescuedCount>().RescuedCount;
        }

        // Dynamically count GameObjects with the tag "Rescuee"
        GameObject[] rescuees = GameObject.FindGameObjectsWithTag("Rescuee");
        int rescueeCount = rescuees.Length; // Number of GameObjects with the "Rescuee" tag

        // Update the TextMeshProUGUI component
        displayText.text = $": {rescuedCount}\n: {rescueeCount}";
    }
}
