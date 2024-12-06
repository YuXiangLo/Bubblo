using UnityEngine;

public class MovementFloatingState : IMovementState
{
    private readonly Player Player;
    private readonly PlayerData Data;


    public MovementFloatingState(Player player, PlayerData data)
    {
        Player = player;
        Data = data;
        Player.SetAnimation(AnimationStateType.Floating);
    }

    public void Enter()
    {
        Player.Velocity = new(0f, 0f);
        Update();
    }

    public void Update()
    {
        if (DetectGround() || DetectClimb() || DetectFall())
        {
            return;
        }
        DetectHorizontalMovement();
        ApplyGravity();
    }

    private bool DetectGround()
    {
        if (Player.Grounded)
        {
            Player.ChangeMovementState(new MovementIdleState(Player, Data));
            return true;
        }
        return false;
    }

    private bool DetectClimb()
    {
        if (Player.IsAbleToClimb && UserInput.Instance.UpKeyDown)
        {
            Player.ChangeMovementState(new MovementClimbingState(Player, Data));
            return true;
        }
        return false;
    }

    private bool DetectFall()
    {
        if (UserInput.Instance.JumpKeyUp)
        {
            Player.ChangeMovementState(new MovementFallingState(Player, Data));
            return true;
        }
        return false;
    }

    private void DetectHorizontalMovement()
    {
        var horizontalInput = UserInput.Instance.HorizontalInput;
        Player.Velocity = new(horizontalInput * Data.MoveSpeed, Player.Velocity.y);
    }

    private void ApplyGravity()
    {
        var gravity = Data.Gravity * Data.LowGravityScale;
        Player.Velocity = new
        (
            Player.Velocity.x, 
            Mathf.Max(Player.Velocity.y + gravity * Time.deltaTime, Data.MaxFloatingYSpeed)
        );
    }
}