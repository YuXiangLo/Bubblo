using UnityEngine;

public class Bubble : MonoBehaviour {
    public float MaxBubbleSize = 3f;
    public float MinBubbleSize = 0.3f;

    [SerializeField] private float MinBubbleSpeed = 4f;
    [SerializeField] private float MaxBubbleSpeed = 10f;
    [SerializeField] private float LifeTime = 5f;
    [SerializeField] private float MinDamage = 3f;
    [SerializeField] private float MaxDamage = 30f;

	private bool IsRelease;
	private float CurrentBubbleSize;
    private Rigidbody2D rb;
    private bool isFacingRight;

    private void Awake() {
        rb = GetComponent<Rigidbody2D>();
    }

    public void InitializeGrowing() {
        transform.localScale = Vector3.one * MinBubbleSize;
        rb.velocity = Vector2.zero;
		IsRelease = false;
    }

    public void UpdateBubbleSize(float holdTime, float maxHoldTime) {
        CurrentBubbleSize = Mathf.Lerp(MinBubbleSize, MaxBubbleSize, holdTime / maxHoldTime);
        transform.localScale = Vector3.one * CurrentBubbleSize;
    }

    public void ReleaseAndShoot(float holdTime, bool playerFacingRight) {
        float bubbleSpeed = Mathf.Lerp(MaxBubbleSpeed, MinBubbleSpeed, holdTime / 2f);
        rb.velocity = new(bubbleSpeed * (playerFacingRight ? 1 : -1), 0);
        Destroy(gameObject, LifeTime);
		IsRelease = true;
    }

	private void OnTriggerEnter2D(Collider2D other) {
		if(other.gameObject.layer == LayerMask.NameToLayer("Enemy")) {
			Debug.Log("Collision detected with " + other.gameObject.name + " on layer " + other.gameObject.layer);
			float damage = Mathf.Lerp(MinDamage, MaxDamage, CurrentBubbleSize / MaxBubbleSize);
			other.gameObject.GetComponent<IModifyHealth>().TakeDamage(damage);
		}
		if(!other.gameObject.CompareTag("Player") && !other.gameObject.CompareTag("Bubble")) {
			Destroy(gameObject);
		}

		if(other.tag == "Player") {
			Debug.Log("Collision detected with " + other.gameObject.name + " on tag " + other.tag);
			if (IsRelease) {
				other.gameObject.GetComponent<Player>().BubbleJump();
				Destroy(gameObject);
			}
		}
	}
}

