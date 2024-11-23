using UnityEngine;
using System;
using System.Collections;

public class PlayerMovementFloatingState : IPlayerMovementState
{
    public Player Player { get; }
    public PlayerData PlayerData { get; }

    private float HorizontalMoveSpeed;

    /// <summary>
    /// Constructor
    /// </summary>
    /// <param name="player">Player</param>
    /// <param name="playerData">PlayerData</param>
    public PlayerMovementFloatingState(Player player, PlayerData playerData)
    {
        Player = player;
        PlayerData = playerData;
        Player.Velocity.x = PlayerData.FloatingXSpeed;
        HorizontalMoveSpeed = PlayerData.MoveSpeed;
        HandleAnimation();
    }

    public void HandleMovement()
    {
        if (Player.IsGrounded)
        {
            Player.ChangePlayerMovementState(new PlayerMovementIdleState(Player, PlayerData));
        } 
		else if (Input.GetKeyUp(KeyCode.W))
        {
            Player.ChangePlayerMovementState(new PlayerMovementFallingState(Player, PlayerData));
        }
        else if (Input.GetKey(KeyCode.W))
        {
            DetectHorizontalMovement();
            ApplyGravity();
        }
    }

    private void DetectHorizontalMovement()
    {
        HorizontalMoveSpeed *= PlayerData.FloatingRatio;
        var horizontalInput = Input.GetAxisRaw("Horizontal");
        Player.Velocity.x = horizontalInput * HorizontalMoveSpeed;
    }

    private void ApplyGravity()
    {
        Player.Velocity.y = Mathf.Max(
            Player.Velocity.y + PlayerData.Gravity * PlayerData.LowGravityScale * Time.deltaTime,
            PlayerData.FloatingYSpeed);
    }
    
	public void HandleAnimation()
	{
        Player.SetAnimation(PlayerStateType.Fall);
    }
}

