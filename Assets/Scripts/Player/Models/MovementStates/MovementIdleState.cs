using UnityEngine;

public class MovementIdleState : IMovementState
{
    private readonly Player Player;
    private readonly PlayerData Data;

    public MovementIdleState(Player player, PlayerData data)
    {
        Player = player;
        Data = data;
        Player.SetAnimation(AnimationStateType.Idle);
    }

    public void Enter()
    {
        Player.Velocity = Vector2.zero;
        Update();
    }

    public void Update()
    {
        if (Player.Grounded)
        {
            if (DetectHorizontalMovement() || DetectJump() || DetectClimb())
            {
                return;
            }
            return;
        }
        
        if (Player.Velocity.y > 0)
        {
            // Wind Case
            Player.ChangeMovementState(new MovementRisingState(Player, Data));
        }
        else if (Player.Velocity.y < 0)
        {
            Player.ChangeMovementState(new MovementFallingState(Player, Data));
        }
    }

    private bool DetectJump()
    {
        if (UserInput.Instance.Jump)
        {
            Player.Velocity = new(0f, Data.JumpForce);
            Player.ChangeMovementState(new MovementRisingState(Player, Data));
            return true;
        }
        return false;
    }

    private bool DetectHorizontalMovement()
    {
        var horizontalInput = UserInput.Instance.Move.x;
        Player.Velocity = new(horizontalInput * Data.MoveSpeed, Player.Velocity.y);
        if (Mathf.Abs(horizontalInput) > 0.01f)
        {
            Player.ChangeMovementState(new MovementRunningState(Player, Data));
            return true;
        }
        return false;
    }

    private bool DetectClimb()
    {
        if (Player.IsAbleToClimb && UserInput.Instance.Move.y > 0.01f)
        {
            Player.ChangeMovementState(new MovementClimbingState(Player, Data));
            return true;
        }
        return false;
    }
}