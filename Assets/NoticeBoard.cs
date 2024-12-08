using UnityEngine;
using TMPro; // Use this if using TextMeshPro

public class NoticeBoardCanvas : MonoBehaviour
{
    [Header("Notice Board Settings")]
    public string message = "Welcome to the Village!"; // Message to display
    public float displayDuration = 3f;                // Time to display the message
    private Canvas canvas;                            // Reference to the Canvas
    private TextMeshProUGUI messageText;              // Reference to the Text component
    private bool isPlayerNearby = false;

    void Start()
    {
        // Get the Canvas and Text components
        canvas = GetComponent<Canvas>();
        messageText = GetComponentInChildren<TextMeshProUGUI>();

        if (canvas != null)
        {
            canvas.enabled = false; // Initially hide the canvas
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            isPlayerNearby = true;
            DisplayMessage();
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            isPlayerNearby = false;
            HideMessage();
        }
    }

    private void DisplayMessage()
    {
        if (canvas != null && messageText != null)
        {
            canvas.enabled = true; // Show the canvas
            messageText.text = message; // Set the message text

            // Optionally hide the text after a duration
            if (displayDuration > 0)
            {
                Invoke(nameof(HideMessage), displayDuration);
            }
        }
    }

    private void HideMessage()
    {
        if (!isPlayerNearby && canvas != null)
        {
            canvas.enabled = false; // Hide the canvas
        }
    }
}
