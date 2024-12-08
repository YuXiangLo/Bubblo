using UnityEngine;

public class MovementEnterLevelState : IMovementState
{
    private readonly Player Player;
    private readonly PlayerData PlayerData;

    public bool AttackEnabled => false;

    private float EnterLevelTimer;
    public MovementEnterLevelState(Player player, PlayerData playerData)
    {
        Player = player;
        PlayerData = playerData;
        EnterLevelTimer = PlayerData.EnterLevelClip.length;
        Player.SetAnimation(AnimationStateType.EnterLevel);
        Player.Velocity = PlayerData.EnterLevelForce;
    }

    public void Enter()
    {
    }

    public void Update()
    {
        EnterLevelTimer -= Time.deltaTime;
        ApplyEnterLevelGravity();
        DiscountVelocity();
        if (EnterLevelTimer <= 0)
        {
            Player.ChangeMovementState(new MovementInitialState(Player, PlayerData));
        }
    }

    private void ApplyEnterLevelGravity()
    {
        Player.Velocity = new Vector2(Player.Velocity.x, Mathf.Max(Player.Velocity.y + PlayerData.EnterLevelGravity * Time.deltaTime, PlayerData.MaxFallingSpeed));
    }

    private void DiscountVelocity()
    {
        Player.Velocity = Player.Velocity * Mathf.Pow(PlayerData.EnterLevelDiscountRatio, Time.deltaTime);
    }
}