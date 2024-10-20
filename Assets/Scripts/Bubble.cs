using UnityEngine;

public class Bubble : MonoBehaviour {
    [SerializeField] private float MinBubbleSpeed = 3f;
    [SerializeField] private float MaxBubbleSpeed = 10f;
    [SerializeField] private float MaxBubbleSize = 3f;
    [SerializeField] private float MinBubbleSize = 0.3f;
    [SerializeField] private float LifeTime = 5f;

    private Rigidbody2D rb;
    private bool isFacingRight;

    private void Awake() {
        rb = GetComponent<Rigidbody2D>();
    }

    // Called when bubble is first instantiated to start growing process
    public void InitializeGrowing(bool playerFacingRight) {
        isFacingRight = playerFacingRight;
        // Start with the minimum size
        transform.localScale = Vector3.one * MinBubbleSize;
        rb.velocity = Vector2.zero; // Bubble should not move while growing
    }

    // Dynamically update the bubble's size while holding the button
    public void UpdateBubbleSize(float holdTime, float maxHoldTime) {
        // Calculate the new size based on how long the button is held
        float sizeFactor = Mathf.Lerp(MinBubbleSize, MaxBubbleSize, holdTime / maxHoldTime);
        transform.localScale = Vector3.one * sizeFactor;
    }

    // Called when the button is released to shoot the bubble
    public void ReleaseAndShoot(float holdTime) {
        // Calculate the speed based on the hold time
        float bubbleSpeed = Mathf.Lerp(MinBubbleSpeed, MaxBubbleSpeed, holdTime / 2f);

        // Set the bubble's velocity based on the direction and speed
        rb.velocity = new Vector2(bubbleSpeed * (isFacingRight ? 1 : -1), 0);

        // Destroy the bubble after its lifetime
        Destroy(gameObject, LifeTime);
    }

	private void OnTriggerEnter2D(Collider2D other)
	{
		if(other.gameObject.layer == LayerMask.NameToLayer("Enemy"))
		{
			Debug.Log("Collision detected with " + other.gameObject.name + " on layer " + other.gameObject.layer);
			other.gameObject.GetComponent<IModifyHealth>().TakeDamage(10f);
			Destroy(gameObject);
		}
        if(other.tag == "Player")
        {
            Debug.Log("Collision detected with " + other.gameObject.name + " on tag " + other.tag);
            other.gameObject.GetComponent<PlayerMovement>().BubbleJump();
        }
	}
}

