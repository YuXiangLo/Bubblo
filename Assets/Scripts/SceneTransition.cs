using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Collections;       

public class SceneTransition : MonoBehaviour
{
    // Name of the scene to load
    public string SceneToLoad;
    
    // Key to press for interaction
    public KeyCode InteractionKey = KeyCode.E;
    
    // Reference to a UI Text component for the interaction message
    public Text InteractionMessage;
    
    // Track if the player is in range
    private bool IsPlayerInRange = false;

    // Reference to the player Rigidbody2D for applying force
    public Rigidbody2D PlayerRigidbody;

    // Reference to the player's movement script (optional)
    public PlayerMovement PlayerMovementScript;
    public MonoBehaviour CameraFollowScript;

    // Blowing force to apply to the player
    public Vector2 BlowForce = new Vector2(900, 1500);
    
    // Time to wait before transitioning scenes
    public float TransitionDelay = 2f;

    private void Start()    
    {
        // Hide the interaction message at the start if it exists
        if (InteractionMessage != null)
        {
            InteractionMessage.gameObject.SetActive(false);
        }
    }

    private void Update()
    {
        // If the player is in range and presses the interaction key, trigger the blow away effect
        if (IsPlayerInRange && Input.GetKeyDown(InteractionKey))
        {
            BlowAwayPlayer();
        }
    }

    private void BlowAwayPlayer()
    {
        // Disable player movement (optional)
        if (PlayerMovementScript != null)
        {
            PlayerMovementScript.enabled = false;
        }

        if (CameraFollowScript) {
            CameraFollowScript.enabled = false;
        }

        // Apply a force to "blow away" the player
        if (PlayerRigidbody != null)
        {
            PlayerRigidbody.gravityScale = 0;           
            PlayerRigidbody.velocity = new Vector2(0, 0);
            PlayerRigidbody.AddForce(BlowForce);
        }

        // Start the coroutine to transition the scene after a delay
        StartCoroutine(TransitionAfterDelay());
    }

    private IEnumerator TransitionAfterDelay()
    {
        // Wait for the specified delay time
        yield return new WaitForSeconds(TransitionDelay);

        // Load the next scene
        SceneManager.LoadScene(SceneToLoad);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            // Player is in range to interact
            IsPlayerInRange = true;

            // Display the interaction message if it exists
            if (InteractionMessage != null)
            {
                InteractionMessage.gameObject.SetActive(true);
                InteractionMessage.text = $"Press '{InteractionKey}' to enter";
            }
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            // Player is out of range
            IsPlayerInRange = false;

            // Hide the interaction message if it exists
            if (InteractionMessage != null)
            {
                InteractionMessage.gameObject.SetActive(false);
            }
        }
    }
}
