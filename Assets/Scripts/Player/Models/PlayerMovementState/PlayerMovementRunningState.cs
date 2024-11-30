using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovementRunningState : IPlayerMovementState
{
    public Player Player { get; }
    public PlayerData PlayerData { get; }

    /// <summary>
    /// Constructor
    /// </summary>
    /// <param name="player">Player</param>
    /// <param name="playerData">PlayerData</param>
    public PlayerMovementRunningState(Player player, PlayerData playerData)
    {
        Player = player;
        Player.Velocity.y = 0f;
        PlayerData = playerData;
        HandleAnimation();
    }
    
    public void HandleMovement()
    {
        if (Player.IsGrounded || Player.IsSlopeMovement)
        {
            if (Mathf.Abs(Player.Velocity.x) <= 0.01f) 
            {
                Player.ChangePlayerMovementState(new PlayerMovementIdleState(Player, PlayerData));
            }
            else 
            {
                DetectHorizontalMovement();
                DetectJumpMovement();
				ApplyGravity();
            }
        }
        else if (Player.Velocity.y > 0)
        {
            Player.ChangePlayerMovementState(new PlayerMovementJumpingState(Player, PlayerData));
        }
        else
        {
            Player.ChangePlayerMovementState(new PlayerMovementFallingState(Player, PlayerData));
        }
    }

    private void DetectJumpMovement()
    {
        if (UserInput.Instance.Jump)
        {
            Player.Velocity.y = Mathf.Max(Player.Velocity.y, 0f) + PlayerData.JumpForce;
        }
    }

    private void DetectHorizontalMovement()
    {
        var horizontalInput = UserInput.Instance.Move.x - UserInput.Instance.Move.y;
        Player.Velocity.x = horizontalInput * PlayerData.MoveSpeed;
		if (Player.SlopeAngle != -1f && Player.SlopeAngle <= 60f) {
			Player.Velocity.x *= Mathf.Cos(Player.SlopeAngle * Mathf.Deg2Rad);
			float slopeSpeedY = Player.Velocity.x * Mathf.Tan(Player.SlopeAngle * Mathf.Deg2Rad) * (Player.CastSide == "Left" ? 1f : -1f);
			if (slopeSpeedY > 0f)
				Player.Velocity.y = Mathf.Max(Player.Velocity.y, slopeSpeedY);
			else if (Player.Velocity.y <= 0f)
				Player.Velocity.y = slopeSpeedY;
		}
    }

    private void ApplyGravity()
    {
        Player.Velocity.y += PlayerData.Gravity * PlayerData.DefaultGravityScale * Time.deltaTime;
    }

	public void HandleAnimation()
	{
        Player.SetAnimation(PlayerStateType.Run);
	}
}
