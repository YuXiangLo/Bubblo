using UnityEngine;

public class MovementRescueState : IMovementState
{
    private readonly Player Player;
    private readonly PlayerData PlayerData;

    private float RescueTimer;

    public bool AttackEnabled => false;

    public MovementRescueState(Player player, PlayerData playerData)
    {
        Player = player;
        PlayerData = playerData;

        RescueTimer = PlayerData.CelebrateClip.length;
        Player.SetAnimation(AnimationStateType.Celebrate);
    }

    public void Enter()
    {
        // TODO: Can add jump force here
        Player.Velocity = new Vector2(Player.Velocity.x, PlayerData.JumpForce);
    }

    public void Update()
    {
        DetectHorizontalInput();
        ApplyGravity();
        RescueTimer -= Time.deltaTime;
        if (RescueTimer <= 0)
        {
            Player.ChangeMovementState(new MovementInitialState(Player, PlayerData));
        }
    }

    private void ApplyGravity()
    {
        Player.Velocity = new Vector2(Player.Velocity.x, Mathf.Max(Player.Velocity.y + PlayerData.Gravity * Time.deltaTime * PlayerData.LowGravityScale, PlayerData.MaxFallingSpeed));
    }

    private void DetectHorizontalInput()
    {
        Player.Velocity = new Vector2(UserInput.Instance.HorizontalInput * PlayerData.MoveSpeed, Player.Velocity.y);
    }
}