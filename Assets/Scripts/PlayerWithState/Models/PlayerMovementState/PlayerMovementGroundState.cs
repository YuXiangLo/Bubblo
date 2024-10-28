using UnityEngine;
using System;
using System.Collections;

public class PlayerMovementGroundState : IPlayerMovementState
{
    public PlayerControl PlayerControl { get; }
    public PlayerData PlayerData { get; }

    public PlayerMovementGroundState(PlayerControl playerControl, PlayerData playerData)
    {
        PlayerControl = playerControl;
        PlayerData = playerData;
    }
    
    public void HandleMovement()
    {
        if (PlayerControl.IsGrounded)
        {
            DetectJumpMovement();
            DetectHorizontalMovement();
            ApplyGravity();
        }
        else if (PlayerControl.Velocity.y > 0)
        {
            PlayerControl.PlayerMovementState = new PlayerMovementJumpingState(PlayerControl, PlayerData);
            PlayerControl.PlayerMovementState.HandleMovement();
        }
        else
        {
            PlayerControl.PlayerMovementState = new PlayerMovementFallingState(PlayerControl, PlayerData);
            PlayerControl.PlayerMovementState.HandleMovement();
        }
    }

    private void DetectJumpMovement()
    {
        if (Input.GetButtonDown("Jump"))
        {
            PlayerControl.Velocity.y = PlayerData.JumpForce;
        }
    }

    private void DetectHorizontalMovement()
    {
        var horizontalInput = Input.GetAxisRaw("Horizontal");
        PlayerControl.Velocity.x = horizontalInput * PlayerData.MoveSpeed;
    }

    private void ApplyGravity()
    {
        PlayerControl.Velocity.y = Mathf.Max(PlayerControl.Velocity.y, 0f);
    }

}

