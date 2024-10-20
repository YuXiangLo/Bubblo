using UnityEngine;

public class Bubble : MonoBehaviour {
    public float MinBubbleSpeed = 3f;
    public float MaxBubbleSpeed = 10f;
    public float MaxBubbleSize = 3f;
    public float MinBubbleSize = 0.3f;
    public float LifeTime = 5f;

    private Rigidbody2D rb;
    private bool isFacingRight;

    private void Awake() {
        rb = GetComponent<Rigidbody2D>();
    }

    public void InitializeGrowing() {
        transform.localScale = Vector3.one * MinBubbleSize;
        rb.velocity = Vector2.zero;
    }

    public void UpdateBubbleSize(float holdTime, float maxHoldTime) {
        float sizeFactor = Mathf.Lerp(MinBubbleSize, MaxBubbleSize, holdTime / maxHoldTime);
        transform.localScale = Vector3.one * sizeFactor;
    }

    public void ReleaseAndShoot(float holdTime, bool playerFacingRight) {
        float bubbleSpeed = Mathf.Lerp(MaxBubbleSpeed, MinBubbleSpeed, holdTime / 2f);
        rb.velocity = new(bubbleSpeed * (playerFacingRight ? 1 : -1), 0);
        Destroy(gameObject, LifeTime);
    }

	private void OnTriggerEnter2D(Collider2D other) {
		if(other.gameObject.layer == LayerMask.NameToLayer("Enemy")) {
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

