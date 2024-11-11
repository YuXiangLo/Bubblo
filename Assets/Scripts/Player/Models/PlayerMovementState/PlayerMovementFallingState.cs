using System;
using System.Collections;
using UnityEngine;

public class PlayerMovementFallingState: IPlayerMovementState
{
    public Player Player { get; }
    public PlayerData PlayerData { get; }

    /// <summary>
    /// Constructor
    /// </summary>
    /// <param name="player">Player</param>
    /// <param name="playerData">PlayerData</param>
    public PlayerMovementFallingState(Player player, PlayerData playerData)
    {
        Player = player;
        PlayerData = playerData;
        HandleAnimation();
    }

    public void HandleMovement()
    {
        if (Player.IsGrounded)
        {
            Player.ChangePlayerMovementState(new PlayerMovementIdleState(Player, PlayerData));
        }
        else if (Player.Velocity.y > 0f)
        {
            Player.ChangePlayerMovementState(new PlayerMovementJumpingState(Player, PlayerData));
        }
        else if (Input.GetButtonDown("Jump"))
        {
            Player.ChangePlayerMovementState(new PlayerMovementFloatingState(Player, PlayerData));
        }
        else 
        {
            DetectHorizontalMovement();
            ApplyGravity();
        }
    }

    private void DetectHorizontalMovement()
    {
        var horizontalInput = Input.GetAxisRaw("Horizontal");
        Player.Velocity.x = horizontalInput * PlayerData.MoveSpeed;
    }

    private void ApplyGravity()
    {
        var gravityScale = Input.GetButton("Jump") ? PlayerData.LowGravityScale : PlayerData.DefaultGravityScale;
        Player.Velocity.y += PlayerData.Gravity * gravityScale * Time.deltaTime;
        
        // Limit Falling Speed
        Player.Velocity.y = Mathf.Max(Player.Velocity.y, PlayerData.MaxFallingSpeed);
    }

	public void HandleAnimation()
	{
		Player.Animator.SetInteger("PlayerState", (int)PlayerState.PlayerStateType.Fall);
	}
}


