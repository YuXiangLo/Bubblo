using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed = 5f;   // Speed of horizontal movement
    public float jumpForce = 30f;  // Force applied for jumping
    public float groundCheckRadius = 0.5f;  // Radius of the ground check
    public Transform groundCheckPosition;   // Position from where we will check for ground
    public LayerMask groundLayer;           // Layer assigned to the ground
    public float playerSize = 0.5f;

    public Rigidbody2D rb;
    public bool isGrounded;
    public Camera mainCamera;

    void Start()
    {
        // Get the Rigidbody2D component    attached to the player
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

        // Check if the player is grounded by performing a circle overlap check
        isGrounded = Physics2D.OverlapCircle(groundCheckPosition.position, groundCheckRadius, groundLayer);

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

    // To visualize the ground check area in the Unity Editor
    void OnDrawGizmos()
    {
        if (groundCheckPosition != null)
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(groundCheckPosition.position, groundCheckRadius);
        }
    }
}
