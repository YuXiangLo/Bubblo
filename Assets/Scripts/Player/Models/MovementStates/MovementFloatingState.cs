using UnityEngine;

public class MovementFloatingState : IMovementState
{
    private readonly Player Player;
    private readonly PlayerData Data;


    public MovementFloatingState(Player player, PlayerData data)
    {
        Player = player;
        Data = data;
    }

    public void Enter()
    {
        Player.Velocity = new(0f, 0f);
    }

    public void Update()
    {
        if (DetectGround() || DetectFall())
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

    private bool DetectFall()
    {
        if (!UserInput.Instance.IsJumpHeld)
        {
            Player.ChangeMovementState(new MovementFallingState(Player, Data));
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
        var gravity = Data.Gravity * Data.LowGravityScale;
        Player.Velocity = new
        (
            Player.Velocity.x, 
            Mathf.Max(Player.Velocity.y + gravity * Time.deltaTime, Data.MaxFloatingYSpeed)
        );
    }
}