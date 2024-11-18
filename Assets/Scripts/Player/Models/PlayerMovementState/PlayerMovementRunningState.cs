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
        if (Player.IsGrounded)
        {
            if (Mathf.Abs(Player.Velocity.x) <= 0.01f) 
            {
                Player.ChangePlayerMovementState(new PlayerMovementIdleState(Player, PlayerData));
            }
            else 
            {
                DetectJumpMovement();
                DetectHorizontalMovement();
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
        if (Input.GetButtonDown("Jump"))
        {
            Player.Velocity.y = PlayerData.JumpForce;
        }
    }

    private void DetectHorizontalMovement()
    {
        var horizontalInput = Input.GetAxisRaw("Horizontal");
        Player.Velocity.x = horizontalInput * PlayerData.MoveSpeed;
    }

	public void HandleAnimation()
	{
        Player.SetAnimation(PlayerStateType.Run);
	}
}
