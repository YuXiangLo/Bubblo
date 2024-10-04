using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed = 5f;   // Speed of horizontal movement
    public float jumpForce = 30f;  // Force applied for jumping
    public float groundCheckThreshold = 0.5f; // How strict we are with "bottom" ground check (adjust as necessary)
    public float playerSize = 0.5f;
    
    private Rigidbody2D rb;
    private bool isGrounded;
    private Camera mainCamera;

    void Start()
    {
        // Get the Rigidbody2D component attached to the player
        rb = GetComponent<Rigidbody2D>();
        rb.freezeRotation = true;

        // Get the main camera in the scene
        mainCamera = Camera.main;
    }

    void Update()
    {
        // Get input from A/D or Left/Right arrow keys for horizontal movement
        float moveInput = Input.GetAxisRaw("Horizontal");  // -1 (left), 1 (right), 0 (no input)

        // Calculate player's new horizontal velocity
        rb.velocity = new Vector2(moveInput * moveSpeed, rb.velocity.y);

        // If the player presses the jump button (space) and is grounded, apply a jump force
        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
        }

        // Restrict the player's horizontal position based on the camera boundaries
        RestrictPlayerWithinCamera();
    }

    void RestrictPlayerWithinCamera()
    {
        // Get the camera's boundaries in world space
        float cameraHalfWidth = mainCamera.orthographicSize * mainCamera.aspect;
        float cameraLeftEdge = mainCamera.transform.position.x - cameraHalfWidth;
        float cameraRightEdge = mainCamera.transform.position.x + cameraHalfWidth;

        // Get the player's current position
        Vector3 playerPosition = transform.position;

        // Clamp the player's x position so they don't go outside the camera's view
        playerPosition.x = Mathf.Clamp(playerPosition.x, cameraLeftEdge + playerSize, cameraRightEdge - playerSize);

        // Apply the clamped position back to the player
        transform.position = playerPosition;
    }

    // This is called when the player collides with another object (like the ground)
    void OnCollisionEnter2D(Collision2D collision)
    {
        // Loop through all contact points to check if any are considered "ground"
        foreach (ContactPoint2D contact in collision.contacts)
        {
            // Check if the collision normal is pointing upwards (indicating ground contact)
            if (contact.normal.y > groundCheckThreshold)
            {
                isGrounded = true;  // Player is grounded
            }
        }
    }

    // This is called when the player stops colliding with another object
    void OnCollisionExit2D(Collision2D collision)
    {
        // If the player stops colliding with the ground, set isGrounded to false   
        isGrounded = false;  // Player is no longer grounded
    }
}
