using UnityEngine;

public class MovementIdleState : IMovementState
{
    private readonly Player Player;
    private readonly PlayerData Data;

    public MovementIdleState(Player player, PlayerData data)
    {
        Player = player;
        Data = data;
    }

    public void Enter()
    {
        Player.SetAnimation(AnimationStateType.Idle);
        Player.Velocity = Vector2.zero;
    }

    public void Update()
    {
        if (Player.Grounded)
        {
            DetectHorizontalMovement();
            DetectJump();
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
            Player.Velocity = new(0f, Data.JumpForce);
            Player.ChangeMovementState(new MovementRisingState(Player, Data));
        }
    }

    private void DetectHorizontalMovement()
    {
        var horizontalInput = UserInput.Instance.Move.x - UserInput.Instance.Move.y;
        Player.Velocity = new(horizontalInput * Data.MoveSpeed, Player.Velocity.y);
        if (Mathf.Abs(horizontalInput) > 0.01f)
        {
            Player.ChangeMovementState(new MovementRunningState(Player, Data));
        }
    }
}