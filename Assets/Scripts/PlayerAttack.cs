using UnityEngine;

public class PlayerAttack : MonoBehaviour {
    [SerializeField] private GameObject BubblePrefab;
    [SerializeField] private float MaxMagicPoint = 10f;

    private float CurrentMagicPoint;
    private Bubble CurrentBubble;
    private float HoldTime = 0f;
    private const float MaxHoldTime = 2f;
	public bool IsButtonHeld = false;
	public bool IsAttack = false;

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
			IsAttack = false;
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
				IsButtonHeld = true;
            }
            else {
                StopChargingBubble();
            }
        } else {
			IsButtonHeld = false;
			IsAttack = false;
		}

        
        if (Input.GetButtonUp("Fire1")) {
            if (CurrentBubble != null) {
                ReleaseBubble();
				IsButtonHeld = false;
				IsAttack = true;
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

