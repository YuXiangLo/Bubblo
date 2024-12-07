using System.Linq;
using Unity.VisualScripting;
using UnityEngine;

public class Bubble : MonoBehaviour
{
    [SerializeField] private float[] Radiuses = new float[] { 0.04f, 0.08f, 0.16f };
    [SerializeField] private float[] Damages = new float[] { 10f, 20f, 60f };
    [SerializeField] private float[] Speeds = new float[] { 11f, 11f, 11f};
    [SerializeField] private float[] ChargingTimes = new float[] { 0.5f, 1.5f, 1.5f };
    [SerializeField] private float[] DiscountRatios = new float[] { 1f, 0.5f, 0.05f };
    [SerializeField] private AnimationClip[] BurstAnimations = new AnimationClip[3];
    [SerializeField] private float LifeTime = 2f;
    [SerializeField] private float CenterOffset = 3f;
    [SerializeField] private string[] IgnoreTags = new string[] { "Player", "Bubble", "Door", "Tools", "Ladder" };

    private Player Player;
    private Animator Animator;
    private CircleCollider2D Collider;
    private bool PlayerFacingRight => Player.IsFacingRight;

    private BubbleSizeType SizeType = BubbleSizeType.Small;
    private BubbleStateType StateType = BubbleStateType.Charge;
    private float ChargingTime = 0f;
    private Vector2 ReleasedVelocity = Vector2.zero;
    private float BurstTimer;
    private bool PassPlayer = false;


    private void Awake()
    {
        Player = FindObjectOfType<Player>();
        Animator = GetComponent<Animator>();
        Collider = GetComponent<CircleCollider2D>();
        Collider.radius = Radiuses[(int)SizeType];
        UpdateHoldingPosition();
    }

    private void FixedUpdate()
    {
        if (StateType is BubbleStateType.Release || StateType is BubbleStateType.Burst)
        {
            transform.position += (Vector3)ReleasedVelocity * Time.fixedDeltaTime;
        }
        else
        {
            UpdateHoldingPosition();
        }
    }

    private void Update()
    {
        if (StateType is BubbleStateType.Charge)
        {
            Charge();
        }
        else if (StateType is BubbleStateType.Release)
        {
            if (PassPlayer is false)
            {
                return;
            }

            // if Burst state, constant velocity
            UpdateReleasedVelocity();
            DetectLifeTime();
        }
        else if (StateType is BubbleStateType.Burst)
        {
            BurstTimer -= Time.deltaTime;
            if (BurstTimer <= 0)
            {
                Destroy(gameObject);
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        int otherLayer = other.gameObject.layer;
        bool isEnemy = otherLayer == LayerMask.NameToLayer("Enemy");
        bool isPlayer = otherLayer == LayerMask.NameToLayer("Player");
        bool isIgnoredTag = IgnoreTags.Contains(other.tag);
        Debug.Log($"OnTriggerEnter2D: {other.tag}");

        if (isEnemy)
        {
            float damage = Damages[(int)SizeType];
            IModifyHealth healthModifier = other.GetComponent<IModifyHealth>();
            healthModifier?.TakeDamage(damage);
            Burst();
        }

        if (isPlayer && StateType is BubbleStateType.Release && SizeType != BubbleSizeType.Small)
        {
            Player.BubbleJump();
            Burst();
        }

        if (isIgnoredTag is false)
        {
            Burst();
            if (StateType != BubbleStateType.Release)
            {
                Player.BubbleBurst();
            }
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject == Player.gameObject)
        {
            PassPlayer = true;
        }
    }

    public void StopCharging() 
    {
        if (StateType is BubbleStateType.Charge)
        {
            StateType = BubbleStateType.Hold;
        }
    }

    public void Release()
    {
        StopCharging();
        StateType = BubbleStateType.Release;
        ReleasedVelocity = new Vector2(Speeds[(int)SizeType] * (PlayerFacingRight ? 1 : -1), 0);
    }

    public void UpdateHoldingPosition() => transform.position = Player.transform.position + CalculateBubblePosition();

    private Vector3 CalculateBubblePosition()
    {
        float bubbleRadius = Radiuses[(int)SizeType];
        float xOffset = (CenterOffset / 2f + bubbleRadius) * (Player.IsFacingRight ? -1 : 1);
        float yOffset = Mathf.Max(0f, bubbleRadius - CenterOffset / 2f + 0.65f);
        return new Vector3(xOffset, yOffset, 0);
    }

    private void Charge()
    {
        var charingAddTime = Mathf.Clamp(ChargingTime + Time.deltaTime, 0f, ChargingTimes[(int)SizeType]) - ChargingTime;
        Player.Consume(charingAddTime);
        ChargingTime += charingAddTime;
        if (ChargingTime == ChargingTimes[(int)SizeType])
        {
            if (SizeType == BubbleSizeType.Medium)
            {
                SizeType = BubbleSizeType.Large;
                StateType = BubbleStateType.Hold;
                UpdateCollider();
                UpdateAnimation();
            }
            else if (SizeType == BubbleSizeType.Small)
            {
                SizeType = BubbleSizeType.Medium;
                UpdateCollider();
                UpdateAnimation();
            }
        }
    }

    private void UpdateReleasedVelocity()
    {
        ReleasedVelocity *= Mathf.Pow(DiscountRatios[(int)SizeType], Time.deltaTime);
    }

    private void DetectLifeTime()
    {
        LifeTime -= Time.fixedDeltaTime;
        if (LifeTime <= 0)
        {
            Burst();
        }
    }
    
    public void Burst()
    {
        SoundManager.PlaySound(SoundType.Bubble, (int)BubbleSoundType.Broken);
        StateType = BubbleStateType.Burst;
        Collider.enabled = false;
        UpdateAnimation();
        BurstTimer = BurstAnimations[(int)SizeType].length;
    }

    private void UpdateAnimation()
    {
        Animator.SetInteger("SizeType", (int)SizeType);
        Animator.SetInteger("StateType", (int)StateType);
    }
    
    private void UpdateCollider() => Collider.radius = Radiuses[(int)SizeType];
}
