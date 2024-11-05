using UnityEngine;
using System;
using System.Collections;

public class PlayerMovementGroundState : IPlayerMovementState
{
    public Player Player { get; }
    public PlayerData PlayerData { get; }

    /// <summary>
    /// Constructor
    /// </summary>
    /// <param name="player">Player</param>
    /// <param name="playerData">PlayerData</param>
    public PlayerMovementGroundState(Player player, PlayerData playerData)
    {
        Player = player;
        PlayerData = playerData;
    }
    
    public void HandleMovement()
    {
        if (Player.IsGrounded)
        {
            DetectJumpMovement();
            DetectHorizontalMovement();
            ApplyGravity();
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
        Player.Animator.SetFloat("Speed", Mathf.Abs(Player.Velocity.x));
    }

    private void ApplyGravity()
    {
        Player.Velocity.y = Mathf.Max(Player.Velocity.y, 0f);
    }

}

