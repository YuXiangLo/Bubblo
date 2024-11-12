using UnityEngine;
using System;
using System.Collections;

public class PlayerMovementIdleState : IPlayerMovementState
{
    public Player Player { get; }
    public PlayerData PlayerData { get; }

    /// <summary>
    /// Constructor
    /// </summary>
    /// <param name="player">Player</param>
    /// <param name="playerData">PlayerData</param>
    public PlayerMovementIdleState(Player player, PlayerData playerData)
    {
        Player = player;
        PlayerData = playerData;
        HandleAnimation();
    }
    
    public void HandleMovement()
    {
        if (Player.IsGrounded)
        {
            if (Mathf.Abs(Player.Velocity.x) > 0.01f) 
            {
                Player.ChangePlayerMovementState(new PlayerMovementRunningState(Player, PlayerData));
            }
            else 
            {
                DetectJumpMovement();
                DetectHorizontalMovement();
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

    private void ApplyGravity()
    {
        Player.Velocity.y = Mathf.Max(Player.Velocity.y, 0f);
    }

	public void HandleAnimation()
	{
		if (!Player.PlayerAttackState.ShouldShowAnimation)
			Player.Animator.SetInteger("PlayerState", (int)PlayerState.PlayerStateType.Idle);
	}
}
