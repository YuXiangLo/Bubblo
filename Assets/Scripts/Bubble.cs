using UnityEngine;

public class Bubble : MonoBehaviour {
    public float MaxBubbleSize = 3f;
    public float MinBubbleSize = 0.3f;

    [SerializeField] private float MinBubbleSpeed = 3f;
    [SerializeField] private float MaxBubbleSpeed = 10f;
    [SerializeField] private float LifeTime = 5f;
    [SerializeField] private float MinDamage = 3f;
    [SerializeField] private float MaxDamage = 30f;

	private float CurrentBubbleSize;
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
        CurrentBubbleSize = Mathf.Lerp(MinBubbleSize, MaxBubbleSize, holdTime / maxHoldTime);
        transform.localScale = Vector3.one * CurrentBubbleSize;
    }

    public void ReleaseAndShoot(float holdTime, bool playerFacingRight) {
        float bubbleSpeed = Mathf.Lerp(MaxBubbleSpeed, MinBubbleSpeed, holdTime / 2f);
        rb.velocity = new(bubbleSpeed * (playerFacingRight ? 1 : -1), 0);
        Destroy(gameObject, LifeTime);
    }

	private void OnTriggerEnter2D(Collider2D other) {
		if(other.gameObject.layer == LayerMask.NameToLayer("Enemy")) {
			Debug.Log("Collision detected with " + other.gameObject.name + " on layer " + other.gameObject.layer);
			float damage = Mathf.Lerp(MinDamage, MaxDamage, CurrentBubbleSize / MaxBubbleSize);
			other.gameObject.GetComponent<IModifyHealth>().TakeDamage(damage);
		}
		if(other.gameObject.layer != LayerMask.NameToLayer("Player")) {
			Destroy(gameObject);
		}
	}
}

