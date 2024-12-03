using UnityEngine;

public class MovementFallingState : IMovementState
{
    private readonly Player Player;
    private readonly PlayerData Data;

    public MovementFallingState(Player player, PlayerData data)
    {
        Player = player;
        Data = data;
    }

    public void Enter()
    {
        Player.SetAnimation(AnimationStateType.Falling);
    }

    public void Update()
    {
        if (DetectGround() || DetectRise() || DetectFloat())
        {
            return;
        }
        DetectHorizontalMovement();
        ApplyGravity();
    }

    private bool DetectFloat()
    {
        if (UserInput.Instance.IsJumpHeld)
        {
            Player.ChangeMovementState(new MovementFloatingState(Player, Data));
            return true;
        }
        return false;
    }

    private bool DetectGround()
    {
        if (Player.Grounded)
        {
            var horizontalInput = UserInput.Instance.Move.x - UserInput.Instance.Move.y;
            Player.Velocity = new(horizontalInput * Data.MoveSpeed, 0f);
            if (Mathf.Abs(Player.Velocity.x) <= 0.01f)
            {
                Player.ChangeMovementState(new MovementIdleState(Player, Data));
            }
            else
            {
                Player.ChangeMovementState(new MovementRunningState(Player, Data));
            }
            return true;
        }
        return false;
    }

    private bool DetectRise()
    {
        if (Player.Velocity.y > 0f)
        {
            Player.ChangeMovementState(new MovementRisingState(Player, Data));
            return true;
        }
        return false;
    }

    private void DetectHorizontalMovement()
    {
        var horizontalInput = UserInput.Instance.Move.x - UserInput.Instance.Move.y;
        Player.Velocity = new(horizontalInput * Data.MoveSpeed, Player.Velocity.y);
    }

    private void ApplyGravity()
    {
        var fallingGravity = Data.Gravity * Data.DefaultGravityScale;
        var velocity = Player.Velocity;
        velocity.y = Mathf.Max(velocity.y + fallingGravity * Time.deltaTime, Data.MaxFallingSpeed);
        Player.Velocity = velocity;
    }
}