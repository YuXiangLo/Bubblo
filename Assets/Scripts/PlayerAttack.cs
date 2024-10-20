using UnityEngine;

public class PlayerAttack : MonoBehaviour {
    [SerializeField] private GameObject BubblePrefab;
    [SerializeField] private float PlayerSize = 1f;
    [SerializeField] private float MaxMagicPoint = 10f;

    private float CurrentMagicPoint;
    private bool IsButtonHeld = false;
    private float ButtonHoldTime = 0f;
    private float MaxBubbleSize;
    private float MinBubbleSize;
    private Player Player;
    private GameObject currentBubble;
    private Rigidbody2D PlayerRigidbody;
    private const float MaxHoldTime = 2f; // Maximum time that affects bubble size and speed

    public float MagicPercentage => CurrentMagicPoint / MaxMagicPoint;

    private void Awake() {
        PlayerRigidbody = GetComponent<Rigidbody2D>();
        Player = GetComponent<Player>();
        Bubble bubbleScript = BubblePrefab.GetComponent<Bubble>();
        MaxBubbleSize = bubbleScript.MaxBubbleSize;
        MinBubbleSize = bubbleScript.MinBubbleSize;
        CurrentMagicPoint = MaxMagicPoint;
    }

    public void HandleAttack() {
        if (Input.GetButtonDown("Fire1")) {
            ButtonHoldTime = 0f;
            IsButtonHeld = true;
            StartGrowingBubble();
        }

        if (Input.GetButton("Fire1") && IsButtonHeld && currentBubble != null && ButtonHoldTime < MaxHoldTime) {
            if (CurrentMagicPoint > 0f) {
                CurrentMagicPoint = Mathf.Clamp(CurrentMagicPoint - Time.deltaTime, 0f, MaxMagicPoint);
                ButtonHoldTime += Time.deltaTime;
            }

            float sizeFactor = Mathf.Lerp(MinBubbleSize, MaxBubbleSize, ButtonHoldTime / MaxHoldTime);
            Vector2 newBubblePosition = CalculateBubblePosition(sizeFactor / 2f);
            currentBubble.transform.position = newBubblePosition;

            if (CurrentMagicPoint > 0f)
            {
                Bubble bubbleScript = currentBubble.GetComponent<Bubble>();
                bubbleScript.UpdateBubbleSize(ButtonHoldTime, MaxHoldTime);
            }
        }

        if (Input.GetButtonUp("Fire1") && IsButtonHeld) {
            IsButtonHeld = false;
            ReleaseBubble();
            ButtonHoldTime = 0f;
        }
    }

    private void StartGrowingBubble() {
        currentBubble = Instantiate(BubblePrefab, CalculateBubblePosition(MinBubbleSize / 2f), Quaternion.identity);
        Bubble bubbleScript = currentBubble.GetComponent<Bubble>();
        bubbleScript.InitializeGrowing();
    }

    private void ReleaseBubble() {
        if (currentBubble != null) {
            float clampedHoldTime = Mathf.Clamp(ButtonHoldTime, 0f, MaxHoldTime);
            Bubble bubbleScript = currentBubble.GetComponent<Bubble>();
            bubbleScript.ReleaseAndShoot(clampedHoldTime, Player.IsFacingRight);
            currentBubble = null;
        }
    }

    private Vector2 CalculateBubblePosition(float bubbleRadius) {
        float xOffset = (PlayerSize / 2f + bubbleRadius) * (Player.IsFacingRight ? 1 : -1);
        float yOffset = Mathf.Max(0f, bubbleRadius - PlayerSize / 2f + 0.05f);
        return new Vector2(PlayerRigidbody.position.x + xOffset, PlayerRigidbody.position.y + yOffset);
    }
}

