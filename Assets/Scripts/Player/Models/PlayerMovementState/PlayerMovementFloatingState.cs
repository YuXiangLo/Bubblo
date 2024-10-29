using UnityEngine;
using System;
using System.Collections;

public class PlayerMovementFloatingState : IPlayerMovementState
{
    public Player Player { get; }
    public PlayerData PlayerData { get; }

    private float HorizontalMoveSpeed;

    public PlayerMovementFloatingState(Player player, PlayerData playerData)
    {
        Player = player;
        PlayerData = playerData;
        Player.Velocity.x = PlayerData.FloatingXSpeed;
        HorizontalMoveSpeed = PlayerData.MoveSpeed;
    }

    public void HandleMovement()
    {
        if (Input.GetButtonUp("Jump"))
        {
            Player.ChangePlayerMovementState(new PlayerMovementFallingState(Player, PlayerData));
        }
        else if (Input.GetButton("Jump"))
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
    
}

