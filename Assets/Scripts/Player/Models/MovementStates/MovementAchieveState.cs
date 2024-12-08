using UnityEngine;

public class MovementAchieveState : IMovementState
{
    private readonly Player Player;
    private readonly PlayerData PlayerData;

    private bool CompletedCelebration = false;
    private float CelebrateTimer;
    private float AchieveTimer;

    public bool AttackEnabled => false;
    
    public MovementAchieveState(Player player, PlayerData playerData)
    {
        Player = player;
        PlayerData = playerData;

        CelebrateTimer = PlayerData.CelebrateClip.length;
        AchieveTimer = PlayerData.AchieveFloatClip.length;
        Player.SetAnimation(AnimationStateType.Celebrate);
    }

    public void Enter()
    {
        // TODO: Can add jump force here
        Player.Velocity = new Vector2(Player.Velocity.x, PlayerData.JumpForce);
    }

    public void Update()
    {
        DiscountVelocityX();
        if (!CompletedCelebration)
        {
            ApplyGravity();
            CelebrateTimer -= Time.deltaTime;
            if (CelebrateTimer <= 0)
            {
                CompletedCelebration = true;
                // Player.SetAnimation(AnimationStateType.AchieveFloat);
                // TODO: Add physics here
            }
        }
        else
        {
            AchieveTimer -= Time.deltaTime;
            if (AchieveTimer <= 0)
            {
                // TODO: Scene transition here
            }
        }
    }

    private void ApplyGravity()
    {
        Player.Velocity = new Vector2(Player.Velocity.x, Mathf.Max(Player.Velocity.y + PlayerData.Gravity * Time.deltaTime * PlayerData.LowGravityScale, PlayerData.MaxFallingSpeed));
    }

    private void DiscountVelocityX()
    {
        var discountRatio = Mathf.Pow(PlayerData.AchieveDiscountRatio, Time.deltaTime);
        Player.Velocity = new Vector2(Player.Velocity.x * discountRatio, Player.Velocity.y);
    }
}