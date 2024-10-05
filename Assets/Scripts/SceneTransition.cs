using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SceneTransition : MonoBehaviour
{
    // Name of the scene to load
    public string sceneToLoad;
    
    // Key to press for interaction
    public KeyCode interactionKey = KeyCode.E;
    
    // Reference to a UI Text component for the interaction message
    public Text interactionMessage;
    
    // Track if the player is in range
    private bool isPlayerInRange = false;

    private void Start()
    {
        // Hide the interaction message at the start if it exists
        if (interactionMessage != null)
        {
            interactionMessage.gameObject.SetActive(false);
        }
    }

    private void Update()
    {
        // If the player is in range and presses the interaction key, load the next scene
        if (isPlayerInRange && Input.GetKeyDown(interactionKey))
        {
            SceneManager.LoadScene(sceneToLoad);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            // Player is in range to interact
            isPlayerInRange = true;

            // Display the interaction message if it exists
            if (interactionMessage != null)
            {
                interactionMessage.gameObject.SetActive(true);
                interactionMessage.text = $"Press '{interactionKey}' to enter";
            }
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            // Player is out of range
            isPlayerInRange = false;

            // Hide the interaction message if it exists
            if (interactionMessage != null)
            {
                interactionMessage.gameObject.SetActive(false);
            }
        }
    }
}

