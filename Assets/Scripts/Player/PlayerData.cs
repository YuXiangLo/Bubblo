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
    public float ClimbingSpeed = 3f;
    public float ClimbFallingSpeed = 5f;
    public float KnockbackForce = 6f;
    public float KnockbackTangent = 2f;
    public float AchieveDiscountRatio = 0.1f;

    // Player Attack
    public GameObject BubblePrefab;
    
    // Animation Clips
    public AnimationClip PitchClip;
    public AnimationClip DieClip;
    public AnimationClip CelebrateClip;
    public AnimationClip AchieveFloatClip;

    public AnimationClip EnterLevelClip;

    // Achieve physics
    public Vector2 AchieveOffset = new Vector2(0f, 0f);
    public float AchieveJumpForce = 24f;
    public float AchieveGravity = -90f;
    public Vector2 AchieveLeavingForce = new Vector2(0f, 0f);

    // Enter Level physics
    public Vector2 EnterLevelOffset = new Vector2(0f, 0f);
    public float EnterLevelGravity = -10f;
    public Vector2 EnterLevelForce = new Vector2(0f, 0f);
    public float EnterLevelDiscountRatio = 0.1f;
}
