using UnityEngine;

public class MovementRunningState : IMovementState
{
    private Player Player;
    private PlayerData Data;

    public bool AttackEnabled => true;

    public MovementRunningState(Player player, PlayerData data)
    {
        Player = player;
        Data = data;
        Player.SetAnimation(AnimationStateType.Running);
    }

    public void Enter()
    {
        Update();
    }

    public void Update()
    {
        if (Player.Grounded || Player.IsSlopeMovement)
        {
            if (DetectJump() || DetectHorizontalMovement() || DetectClimb())
            {
                return;
            }
            ApplyGravity();
            return;
        }
        
        if (Player.Velocity.y > 0)
        {
            Player.ChangeMovementState(new MovementRisingState(Player, Data));
        }
        else
        {
            Player.ChangeMovementState(new MovementFallingState(Player, Data));
        }
    }

    private bool DetectJump()
    {
        if (UserInput.Instance.JumpKeyDown)
        {
            Player.Velocity = new(Player.Velocity.x, Data.JumpForce);
            Player.ChangeMovementState(new MovementRisingState(Player, Data));
            return true;
        }
        return false;
    }

    /// <summary>
    /// Detects horizontal movement and changes the state to idle if there is no input.
    /// </summary>
    /// <returns>return <c>true</c> if no horizontal input</returns>
    private bool DetectHorizontalMovement()
    {
        var horizontalInput = UserInput.Instance.HorizontalInput;
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

    private void ApplyGravity()
    {
        var gravity = Data.Gravity * Data.DefaultGravityScale;
        Player.Velocity = new(Player.Velocity.x, Player.Velocity.y + gravity * Time.deltaTime);
    }
}
