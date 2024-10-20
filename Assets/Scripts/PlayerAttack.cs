using UnityEngine;

public class PlayerAttack : MonoBehaviour {
    [SerializeField] private GameObject BubblePrefab;
    [SerializeField] private float MinBubbleSpeed = 3f;
    [SerializeField] private float MaxBubbleSpeed = 10f;
    [SerializeField] private float MaxBubbleSize = 3f;
    [SerializeField] private float MinBubbleSize = 0.3f;
    [SerializeField] private float BubbleLifetime = 2f;
    [SerializeField] private float PlayerSize = 1f;

    private const float MaxHoldTime = 2f; // Maximum time that affects bubble size and speed

    private float ButtonHoldTime = 0f;
    private bool IsButtonHeld = false;
    private Player Player;

    private Rigidbody2D PlayerRigidbody;

    private void Awake() {
        PlayerRigidbody = GetComponent<Rigidbody2D>();
        Player = GetComponent<Player>();
    }

    // Handles bubble instantiation, scaling, and movement
    private void ThrowBubble(float sizeFactor, float bubbleSpeed) {
        float bubbleRadius = sizeFactor / 2f;
        float xOffset = (PlayerSize / 2f + bubbleRadius) * (Player.IsFacingRight ? 1 : -1);
        float yOffset = Mathf.Max(0f, bubbleRadius - PlayerSize / 2f);

        // Spawn position adjusts both x and y to accommodate bubble size
        Vector2 spawnPosition = new Vector2(PlayerRigidbody.position.x + xOffset, PlayerRigidbody.position.y + yOffset);
        GameObject bubble = Instantiate(BubblePrefab, spawnPosition, Quaternion.identity);
        
        // Scale the bubble according to the size factor (proportional to hold time)
        bubble.transform.localScale = Vector3.one * sizeFactor;

        // Set the bubble's velocity based on facing direction
        Rigidbody2D rb = bubble.GetComponent<Rigidbody2D>();
        rb.velocity = new Vector2(bubbleSpeed * (Player.IsFacingRight ? 1 : -1), 0);

        Destroy(bubble, BubbleLifetime);
    }

    // Method to be called when the button is released to throw a bubble
    private void ReleaseBubble() {
        // Clamp ButtonHoldTime to avoid excessively large bubbles
        float clampedHoldTime = Mathf.Clamp(ButtonHoldTime, 0f, MaxHoldTime);

        // Calculate size and speed based on how long the button was held
        float sizeFactor = Mathf.Lerp(MinBubbleSize, MaxBubbleSize, clampedHoldTime / MaxHoldTime);
        float bubbleSpeed = Mathf.Lerp(MaxBubbleSpeed, MinBubbleSpeed, clampedHoldTime / MaxHoldTime);

        // Throw the bubble with calculated size and speed
        ThrowBubble(sizeFactor, bubbleSpeed);
    }

    // Method to handle the player's attack input
    public void HandleAttack() {
        if (Input.GetButtonDown("Fire1")) {
            ButtonHoldTime = 0f;
            IsButtonHeld = true;
        }

        if (Input.GetButton("Fire1") && IsButtonHeld) {
            ButtonHoldTime += Time.deltaTime;
        }

        if (Input.GetButtonUp("Fire1") && IsButtonHeld) {
            IsButtonHeld = false;
            ReleaseBubble();
            ButtonHoldTime = 0f;
        }
    }
}

