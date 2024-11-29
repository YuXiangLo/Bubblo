using System.Linq;
using UnityEngine;

public class Bubble : MonoBehaviour
{
    // SerializeField
    #region BubbleSize
    [SerializeField] private float MaxSize = 1.3f;
    [SerializeField] private float MinSize = 0.2f;
    #endregion

    #region BubbleSpeedLifeTime
    [SerializeField] private float MinSpeed = 8f;
    [SerializeField] private float MaxSpeed = 11f;
    [SerializeField] private float LifeTime = 2f;
    #endregion

    #region BubbleDamage
    [SerializeField] private float MinDamage = 3f;
    [SerializeField] private float MaxDamage = 30f;
    [SerializeField] private float DiscountRatio = 0.98f; // 0.98f for macbook unity, 0.97f for linux unity
    #endregion

    // Private const or readonly
    private Player Player;
    private const float PLAYER_SIZE = 1f;
    private const float MAX_CHARGING_TIME = 1f;

    // Private variables
    private bool PlayerFacingRight => Player.IsFacingRight;
    private bool IsCharging = true;
    private bool IsRelease = false;
    private float ChargingTime = 0f;
    private float CurrentSize => Mathf.Lerp(MinSize, MaxSize, ChargingTime / MAX_CHARGING_TIME);
    private float ReleaseSpeed => Mathf.Lerp(MaxSpeed, MinSpeed, ChargingTime / MAX_CHARGING_TIME);
    private Rigidbody2D Rb;
    private string[] IgnoreTags = new string[] { "Player", "Bubble", "Door", "Tools" };

    private void Awake()
    {
        Player = FindObjectOfType<Player>();
        transform.localScale = Vector3.one * MinSize;
        Rb = GetComponent<Rigidbody2D>();
        Rb.velocity = Vector2.zero;
        UpdateHoldingPosition();
    }

    private void FixedUpdate()
    {
        if (IsRelease && CurrentSize >= PLAYER_SIZE)
        {
            Rb.velocity = new (Rb.velocity.x * DiscountRatio, 0);
        }
    }

    private void Update()
    {
        if (IsRelease)
        {
            return;
        }
            
        if (IsCharging) {
            ChargingTime += Time.deltaTime;
            UpdateSize();
        }
        UpdateHoldingPosition();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Enemy")) {
            float damage = Mathf.Lerp(MinDamage, MaxDamage, CurrentSize / MaxSize);
            other.gameObject.GetComponent<IEnemyModifyHealth>().TakeDamage(damage);
        }

        bool shouldDestroy = !IgnoreTags.Any(tag => other.gameObject.CompareTag(tag));

        if (shouldDestroy) {
            Destroy(gameObject);
            if (!IsRelease) {
                Player.BubbleDestroyed();
            }
        }

        if (other.gameObject.layer == LayerMask.NameToLayer("Player")) {
            if (IsRelease) {
                other.gameObject.GetComponent<Player>().BubbleJump();
                Destroy(gameObject);
            }
        }
    }

    public void StopCharging() => IsCharging = false;
    public void Remove() => Destroy(gameObject);

    public void Release()
    {
        IsRelease = true;
        Rb.velocity = new(ReleaseSpeed * (PlayerFacingRight ? 1 : -1), 0);
        Destroy(gameObject, LifeTime);
    }

    private void UpdateSize() => transform.localScale = Vector3.one * CurrentSize;
    private void UpdateHoldingPosition() => transform.position = Player.transform.position + CalculateBubblePosition();

    private Vector3 CalculateBubblePosition()
    {
        float bubbleRadius = CurrentSize / 2f;
        float xOffset = (PLAYER_SIZE / 2f + bubbleRadius) * (Player.IsFacingRight ? 1 : -1);
        float yOffset = Mathf.Max(0f, bubbleRadius - PLAYER_SIZE / 2f + 0.05f);
        return new Vector3(xOffset, yOffset, 0);
    }
}

