using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Collections;       

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

    // Reference to the player Rigidbody2D for applying force
    public Rigidbody2D playerRigidbody;

    // Reference to the player's movement script (optional)
    public MonoBehaviour playerMovementScript;
    public MonoBehaviour CameraFollowScript;

    // Blowing force to apply to the player
    public Vector2 blowForce = new Vector2(900, 1500);
    
    // Time to wait before transitioning scenes
    public float transitionDelay = 2f;

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
        // If the player is in range and presses the interaction key, trigger the blow away effect
        if (isPlayerInRange && Input.GetKeyDown(interactionKey))
        {
            BlowAwayPlayer();
        }
    }

    private void BlowAwayPlayer()
    {
        // Disable player movement (optional)
        if (playerMovementScript != null)
        {
            playerMovementScript.enabled = false;
        }

        if (CameraFollowScript) {
            CameraFollowScript.enabled = false;
        }

        // Apply a force to "blow away" the player
        if (playerRigidbody != null)
        {
            playerRigidbody.gravityScale = 0;           
            playerRigidbody.velocity = new Vector2(0, 0);
            playerRigidbody.AddForce(blowForce);
        }

        // Start the coroutine to transition the scene after a delay
        StartCoroutine(TransitionAfterDelay());
    }

    private IEnumerator TransitionAfterDelay()
    {
        // Wait for the specified delay time
        yield return new WaitForSeconds(transitionDelay);

        // Load the next scene
        SceneManager.LoadScene(sceneToLoad);
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
