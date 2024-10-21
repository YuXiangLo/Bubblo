using UnityEngine;

public class PlayerAttack : MonoBehaviour {
    [SerializeField] private GameObject BubblePrefab;
    [SerializeField] private float MaxMagicPoint = 10f;

    private float CurrentMagicPoint;
    private Bubble CurrentBubble;
    private float HoldTime = 0f;
    private const float MaxHoldTime = 2f;

    public float MagicPercentage => CurrentMagicPoint / MaxMagicPoint;

    private void Awake()
    {
        CurrentMagicPoint = MaxMagicPoint;

        CurrentBubble = null;
    }

    public void HandleAttack()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            Debug.Log("MagicPercentage" + MagicPercentage);
            InitialBubble();
            if (MagicPercentage <= 0) {
                StopChargingBubble();
                ReleaseBubble();
            }
        }

        if (CurrentBubble != null)
        {
            if (CurrentMagicPoint > 0 && HoldTime < MaxHoldTime) {
                CurrentMagicPoint -= Time.deltaTime;
                HoldTime += Time.deltaTime;
            }
            else {
                StopChargingBubble();
            }
        }
        
        if (Input.GetButtonUp("Fire1")) {
            if (CurrentBubble != null) {
                ReleaseBubble();
            }
        }
    }

    private void InitialBubble() {
        CurrentBubble = Instantiate(BubblePrefab).GetComponent<Bubble>();
        HoldTime = 0f;
    }
    
    private void StopChargingBubble() {
        CurrentBubble.StopCharging();
    }
    private void ReleaseBubble() {
        CurrentBubble.Release();
        CurrentBubble = null;
    }
}

