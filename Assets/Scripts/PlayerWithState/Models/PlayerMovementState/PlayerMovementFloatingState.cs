using UnityEngine;
using System;
using System.Collections;

public class PlayerMovementFloatingState : IPlayerMovementState
{
    public PlayerControl PlayerControl { get; }
    public PlayerData PlayerData { get; }

    public PlayerMovementFloatingState(PlayerControl playerControl, PlayerData playerData)
    {
        PlayerControl = playerControl;
        PlayerData = playerData;
        PlayerControl.Velocity.x = PlayerData.FloatingXSpeed;
    }

    public void HandleMovement()
    {
        if (Input.GetButtonUp("Jump"))
        {
            PlayerControl.ChangePlayerMovementState(new PlayerMovementFallingState(PlayerControl, PlayerData));
        }
        else if (Input.GetButton("Jump"))
        {
            DetectHorizontalMovement();
            ApplyGravity();
        }
    }

    private void DetectHorizontalMovement()
    {
        PlayerControl.Velocity.x *= PlayerData.FloatingRatio;
    }

    private void ApplyGravity()
    {
        PlayerControl.Velocity.y = Mathf.Max(
            PlayerControl.Velocity.y + PlayerData.Gravity * PlayerData.LowGravityScale * Time.deltaTime,
            PlayerData.FloatingYSpeed);
    }
    
}

