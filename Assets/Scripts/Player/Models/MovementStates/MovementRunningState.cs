using UnityEngine;

public class MovementRunningState : IMovementState
{
    private Player Player;
    private PlayerData Data;

    public MovementRunningState(Player player, PlayerData data)
    {
        Player = player;
        Data = data;
    }

    public void Enter()
    {
        DetectHorizontalMovement();
    }

    public void Update()
    {
        if (Player.Grounded || Player.IsSlopeMovement)
        {
            DetectHorizontalMovement();
            DetectJump();
            ApplyGravity();
        }
        else if (Player.Velocity.y > 0)
        {
            Player.ChangeMovementState(new MovementRisingState(Player, Data));
        }
        else
        {
            Player.ChangeMovementState(new MovementFallingState(Player, Data));
        }
    }

    private void DetectJump()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Player.Velocity = new(Player.Velocity.x, Data.JumpForce);
            Debug.Log($"Jumping with velocity: {Player.Velocity}");
            Player.ChangeMovementState(new MovementRisingState(Player, Data));
        }
    }

    private void DetectHorizontalMovement()
    {
        var horizontalInput = UserInput.Instance.Move.x - UserInput.Instance.Move.y;
        var velocity = Player.Velocity;
        velocity.x = horizontalInput * Data.MoveSpeed;
        if (Player.SlopeAngle != -1f && Player.SlopeAngle <= 60f)
        {
            velocity.x *= Mathf.Cos(Player.SlopeAngle * Mathf.Deg2Rad);
            float slopeSpeedY = velocity.x * Mathf.Tan(Player.SlopeAngle * Mathf.Deg2Rad) * (Player.CastSide == CastSide.Left ? 1f : -1f);
            if (slopeSpeedY > 0f)
                velocity.y = Mathf.Max(velocity.y, slopeSpeedY);
            else if (velocity.y <= 0f)
                velocity.y = slopeSpeedY;
        }
        Player.Velocity = velocity;
        if (Mathf.Abs(horizontalInput) <= 0.01f)
        {
            Player.ChangeMovementState(new MovementIdleState(Player, Data));
        }
    }

    private void ApplyGravity()
    {
        var gravity = Data.Gravity * Data.DefaultGravityScale;
        Player.Velocity = new(Player.Velocity.x, Player.Velocity.y + gravity * Time.deltaTime);
    }
}