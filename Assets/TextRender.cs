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

        // Safely retrieve the LevelData's RescueeCount
        LevelData levelData = FindObjectOfType<LevelData>();
        int rescueeCount = 0; // Default value in case LevelData is not found
        if (levelData != null)
        {
            rescueeCount = levelData.RescueeCount;
        }

        // Update the TextMeshProUGUI component
        displayText.text = $": {rescuedCount}\n: {rescueeCount}";
    }
}
