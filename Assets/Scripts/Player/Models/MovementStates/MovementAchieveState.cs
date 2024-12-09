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
        Player.Velocity = new Vector2(Player.Velocity.x, PlayerData.AchieveJumpForce);
    }

    public void Update()
    {
        if (!CompletedCelebration)
        {
            DiscountVelocityX();
            ApplyGravity();
            CelebrateTimer -= Time.deltaTime;
            if (CelebrateTimer <= 0)
            {
                Player.CameraEndLevel = true;
                CompletedCelebration = true;
                Player.SetAnimation(AnimationStateType.AchieveFloat);
                Player.transform.position += (Vector3)PlayerData.AchieveOffset;
            }
        }
        else
        {
            ApplyLeavingForce();
            AchieveTimer -= Time.deltaTime;
            if (AchieveTimer <= 0)
            {
                //TODO: Add scene transition here
				Player.GameOverPanel.SetActive(true);
            }
        }
    }

    private void ApplyGravity()
    {
        Player.Velocity = new Vector2(Player.Velocity.x, Mathf.Max(Player.Velocity.y + PlayerData.AchieveGravity * Time.deltaTime, PlayerData.MaxFallingSpeed));
    }

    private void ApplyLeavingForce()
    {
        float velocityX = Player.Velocity.x + PlayerData.AchieveLeavingForce.x * Time.deltaTime;

        float velocityY = Mathf.Max(
            Player.Velocity.y + PlayerData.AchieveLeavingForce.y * Time.deltaTime,
            PlayerData.MaxFallingSpeed
        );

        Player.Velocity = new Vector2(velocityX, velocityY);
    }


    private void DiscountVelocityX()
    {
        var discountRatio = Mathf.Pow(PlayerData.AchieveDiscountRatio, Time.deltaTime);
        Player.Velocity = new Vector2(Player.Velocity.x * discountRatio, Player.Velocity.y);
    }
}
