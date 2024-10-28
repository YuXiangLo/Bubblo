using UnityEngine;

public class PlayerData : MonoBehaviour
{
    public float MoveSpeed = 8f;
	public float FloatingXSpeed = 12f;
    public float FloatingYSpeed = -2f;
    public float FloatingRatio = 0.9995f;
    public float Gravity = -90f;
    public float MaxFallingSpeed = -20f;
    public float JumpForce = 18f;
    public float PlayerSize = 1f; 
	public float TriggerDistance = 0.001f;
    public float DefaultGravityScale = 1f;
    public float LowGravityScale = 0.5f;
    public float KnockbackForce = 6f;
    public float KnockbackTangent = 2f;
}
