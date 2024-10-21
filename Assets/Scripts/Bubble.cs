using UnityEngine;

public class Bubble : MonoBehaviour {
    public float MaxSize = 3f;
    public float MinSize = 0.3f;

    [SerializeField] private float MinSpeed = 4f;
    [SerializeField] private float MaxSpeed = 10f;
    [SerializeField] private float LifeTime = 5f;
    [SerializeField] private float MinDamage = 3f;
    [SerializeField] private float MaxDamage = 30f;

    private Player Player;
    private bool PlayerFacingRight => Player.IsFacingRight;
    private float PlayerSize = 1f;

    private bool IsCharging = true;
	private bool IsRelease = false;
    private float ChargingTime = 0f;
    private const float MaxChargingTime = 2f;
	private float CurrentSize => Mathf.Lerp(MinSize, MaxSize, ChargingTime / MaxChargingTime);
    private float ReleaseSpeed => Mathf.Lerp(MinSpeed, MaxSpeed, ChargingTime / MaxChargingTime);
    private Rigidbody2D rb;

    private void Awake() {
        Player = FindObjectOfType<Player>();
        rb = GetComponent<Rigidbody2D>();
        transform.localScale = Vector3.one * MinSize;
        rb.velocity = Vector2.zero;
        UpdatePosition();
    }

    private void Update() {
        if (!IsRelease) {
            if (IsCharging) {
                ChargingTime += Time.deltaTime;
                Debug.Log(ChargingTime);
                UpdateSize();
            }
            UpdatePosition();
        }
    }

    private void OnTriggerEnter2D(Collider2D other) {
		if(other.gameObject.layer == LayerMask.NameToLayer("Enemy")) {
			float damage = Mathf.Lerp(MinDamage, MaxDamage, CurrentSize / MaxSize);
			other.gameObject.GetComponent<IModifyHealth>().TakeDamage(damage);
		}
		if(!other.gameObject.CompareTag("Player") && !other.gameObject.CompareTag("Bubble")) {
			Destroy(gameObject);
		}

		if(other.gameObject.layer == LayerMask.NameToLayer("Player")) {
			if (IsRelease) {
				other.gameObject.GetComponent<Player>().BubbleJump();
				Destroy(gameObject);
			}
		}
	}

    public void StopCharging() {
        IsCharging = false;
    }

    public void Release() {
        IsRelease = true;
        rb.velocity = new(ReleaseSpeed * (PlayerFacingRight ? 1 : -1), 0);
        Destroy(gameObject, LifeTime);
    }

    private void UpdateSize() {
        transform.localScale = Vector3.one * CurrentSize;
    }
    private void UpdatePosition() {
        transform.position = Player.transform.position + CalculateBubblePosition();
    }

	private Vector3 CalculateBubblePosition() {
        float bubbleRadius = CurrentSize / 2f;
        float xOffset = (PlayerSize / 2f + bubbleRadius) * (Player.IsFacingRight ? 1 : -1);
        float yOffset = Mathf.Max(0f, bubbleRadius - PlayerSize / 2f + 0.05f);
        return new Vector3(xOffset, yOffset, 0);
    }
}

