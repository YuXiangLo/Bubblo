using UnityEngine;

public class PlayerAttack : MonoBehaviour {
    [SerializeField] private GameObject BubblePrefab;
    [SerializeField] private float PlayerSize = 1f;

    private const float MaxHoldTime = 2f; // Maximum time that affects bubble size and speed
    private float ButtonHoldTime = 0f;
    private bool IsButtonHeld = false;
    private GameObject currentBubble; // Reference to the bubble currently being grown
    private Player Player;
    private Rigidbody2D PlayerRigidbody;
    [SerializeField] private float MaxBubbleSize = 3f;
    [SerializeField] private float MinBubbleSize = 0.3f;

    private void Awake() {
        PlayerRigidbody = GetComponent<Rigidbody2D>();
        Player = GetComponent<Player>();
    }

    // Method to instantiate and start growing the bubble
    private void StartGrowingBubble() {
        Vector2 spawnPosition = CalculateBubblePosition(MinBubbleSize / 2f); // Initial spawn with minimum bubble size
        currentBubble = Instantiate(BubblePrefab, spawnPosition, Quaternion.identity);

        // Initialize the bubble but don't shoot yet, just grow
        Bubble bubbleScript = currentBubble.GetComponent<Bubble>();
        bubbleScript.InitializeGrowing(Player.IsFacingRight);
    }

    // Method to stop growing and shoot the bubble
    private void ReleaseBubble() {
        if (currentBubble != null) {
            // Clamp ButtonHoldTime to avoid excessively large bubbles
            float clampedHoldTime = Mathf.Clamp(ButtonHoldTime, 0f, MaxHoldTime);

            // Pass the final hold time to the bubble to shoot it
            Bubble bubbleScript = currentBubble.GetComponent<Bubble>();
            bubbleScript.ReleaseAndShoot(clampedHoldTime);

            currentBubble = null; // Reset the reference to current bubble
        }
    }

    // Dynamically calculate the bubble position based on its size and the player's facing direction
    private Vector2 CalculateBubblePosition(float bubbleRadius) {
        float xOffset = (PlayerSize / 2f + bubbleRadius) * (Player.IsFacingRight ? 1 : -1);
        float yOffset = Mathf.Max(0f, bubbleRadius - PlayerSize / 2f);

        return new Vector2(PlayerRigidbody.position.x + xOffset, PlayerRigidbody.position.y + yOffset);
    }

    // Method to handle the player's attack input
    public void HandleAttack() {
        if (Input.GetButtonDown("Fire1")) {
            ButtonHoldTime = 0f;
            IsButtonHeld = true;
            StartGrowingBubble();
        }

        if (Input.GetButton("Fire1") && IsButtonHeld) {
            ButtonHoldTime += Time.deltaTime;

            // Update the bubble's size and position dynamically based on hold time
            if (currentBubble != null) {
                float sizeFactor = Mathf.Lerp(MinBubbleSize, MaxBubbleSize, ButtonHoldTime / MaxHoldTime);
                Vector2 newBubblePosition = CalculateBubblePosition(sizeFactor / 2f);

                Bubble bubbleScript = currentBubble.GetComponent<Bubble>();
                bubbleScript.UpdateBubbleSize(ButtonHoldTime, MaxHoldTime);
                currentBubble.transform.position = newBubblePosition; // Update position
            }
        }

        if (Input.GetButtonUp("Fire1") && IsButtonHeld) {
            IsButtonHeld = false;
            ReleaseBubble();
            ButtonHoldTime = 0f;
        }
    }
}

