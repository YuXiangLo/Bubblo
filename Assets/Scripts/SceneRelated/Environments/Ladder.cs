using UnityEngine;

public class Ladder : MonoBehaviour
{
    private bool isNearLadder = false; // Whether the player is near the ladder
    private bool isClimbing = false; // Whether the player is climbing
    private Player player; // Reference to the player's script
    private PlayerData playerData; // Reference to the player's data
    public float climbSpeed = 5f; // Speed at which the player climbs

    // Ladder size
    private float hittingPoint = 24.5f;

    void Start()
    {
        
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            player = collision.GetComponent<Player>();
            playerData = collision.GetComponent<PlayerData>();
            isNearLadder = true;
        }
    }

    void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            isNearLadder = false;
            StopClimbing();
        }
    }

    void Update()
    {
        if (isNearLadder && UserInput.Instance.Interact)
        {
            // Check if the player fully covers the ladder
            if (IsPlayerFullyCoveringLadder())
            {
                // Toggle climbing
                isClimbing = !isClimbing;

                if (isClimbing)
                {
                    StartClimbing();
                }
                else
                {
                    StopClimbing();
                }
            }
        }

        if (isClimbing && player != null)
        {
            float vertical = Input.GetAxis("Vertical"); // Get vertical input
            player.Velocity.y = vertical * climbSpeed; // Modify the player's vertical velocity
            player.Velocity.x = 0; // Stop horizontal movement
        }
    }

    private bool IsPlayerFullyCoveringLadder()
    {
        if (player == null)
            return false;

        Debug.Log(player.transform.position.x + " " + hittingPoint);
        return player.transform.position.x < hittingPoint + 0.3f && player.transform.position.x > hittingPoint - 0.3f;
    }

    private void StartClimbing()
    {
        if (playerData != null)
        {
            playerData.Gravity = 0; // Disable gravity for climbing
        }
    }

    private void StopClimbing()
    {
        if (playerData != null)
        {
            playerData.Gravity = -90; // Re-enable gravity (adjust value as needed)
        }
        isClimbing = false; // Ensure climbing stops
    }
}
