using System.Collections;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;

public class Bubble : MonoBehaviour
{
    [SerializeField] private float[] Radiuses = new float[] { 0.04f, 0.08f, 0.16f };
    [SerializeField] private float[] Damages = new float[] { 10f, 20f, 60f };
    [SerializeField] private float[] Speeds = new float[] { 11f, 9.5f, 8f};
    [SerializeField] private float[] ChargingTimes = new float[] { 0.5f, 1.5f, 1.5f };
    [SerializeField] private float DiscountRatio = 0.2f;
    [SerializeField] private float LifeTime = 2f;
    [SerializeField] private float PlayerSize = 3f;

    private Player Player;
    private Animator Animator;
    private CircleCollider2D Collider;
    private bool PlayerFacingRight => Player.IsFacingRight;

    private BubbleSizeType SizeType = BubbleSizeType.Small;
    private BubbleStateType StateType = BubbleStateType.Charge;
    private float ChargingTime = 0f;
    private Vector2 ReleasedVelocity = Vector2.zero;

    private readonly string[] IgnoreTags = new string[] { "Player", "Bubble", "Door", "Tools", "Ladder" };

    private void Awake()
    {
        Player = FindObjectOfType<Player>();
        Animator = GetComponent<Animator>();
        Collider = GetComponent<CircleCollider2D>();
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
            // if Burst state, constant velocity
            UpdateReleasedVelocity();
            DetectLifeTime();
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        int otherLayer = other.gameObject.layer;
        bool isEnemy = otherLayer == LayerMask.NameToLayer("Enemy");
        bool isPlayer = otherLayer == LayerMask.NameToLayer("Player");
        bool isIgnoredTag = IgnoreTags.Contains(other.tag);

        if (isEnemy)
        {
            float damage = Damages[(int)SizeType];
            IModifyHealth healthModifier = other.GetComponent<IModifyHealth>();
            healthModifier?.TakeDamage(damage);
            Burst();
        }

        if (isPlayer && StateType is BubbleStateType.Release)
        {
            Player.BubbleJump();
            Burst();
        }

        if(isIgnoredTag is false)
        {
            Burst();
            if (StateType != BubbleStateType.Release)
            {
                Player.BubbleBurst();
            }
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

    private void UpdateHoldingPosition() => transform.position = Player.transform.position + CalculateBubblePosition();

    private Vector3 CalculateBubblePosition()
    {
        float bubbleRadius = Radiuses[(int)SizeType];
        float xOffset = (PlayerSize / 2f + bubbleRadius) * (Player.IsFacingRight ? 1 : -1);
        float yOffset = Mathf.Max(0f, bubbleRadius - PlayerSize / 2f + 0.05f);
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
        if (SizeType == BubbleSizeType.Large)
        {
            ReleasedVelocity *= Mathf.Pow(DiscountRatio, Time.deltaTime);
        }
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
        StateType = BubbleStateType.Burst;
        Collider.enabled = false;
        UpdateAnimation();
        StartCoroutine(BurstCoroutine());
    }

    private void UpdateAnimation()
    {
        Animator.SetInteger("SizeType", (int)SizeType);
        Animator.SetInteger("StateType", (int)StateType);
    }
    
    private void UpdateCollider()
    {
        Collider.radius = Radiuses[(int)SizeType];
    }

    private IEnumerator BurstCoroutine()
    {
        var stateInfo = Player.Animator.GetCurrentAnimatorStateInfo(0);
        float remainingTime = stateInfo.length * (1f - stateInfo.normalizedTime);
        yield return new WaitForSeconds(remainingTime);
        Destroy(gameObject);
    }
}
