using UnityEngine;

public class PlayerData : MonoBehaviour
{
    // Player Movement
    public float MoveSpeed = 6f;
    public float MaxFloatingYSpeed = -2f;
    public float Gravity = -90f;
    public float MaxFallingSpeed = -20f;
    public float MinBlowingSpeed = 20f;
    public float JumpForce = 18f;
    public float BubbleJumpForce = 24f;
    public float PlayerSize = 1f; 
    public float DefaultGravityScale = 1f;
    public float LowGravityScale = 0.5f;
    public float KnockbackForce = 6f;
    public float KnockbackTangent = 2f;

    // Player Attack
    public GameObject BubblePrefab;
    public const float CHARGE_INTERVAL = 0.4f;
}
