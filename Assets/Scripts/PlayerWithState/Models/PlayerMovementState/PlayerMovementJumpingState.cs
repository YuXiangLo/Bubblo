using System;
using System.Collections;
using UnityEngine;

public class PlayerMovementJumpingState: IPlayerMovementState
{
    public PlayerControl PlayerControl { get; }
    public PlayerData PlayerData { get; }

    public PlayerMovementJumpingState(PlayerControl playerControl, PlayerData playerData)
    {
        PlayerControl = playerControl;
        PlayerData = playerData;
    }

    public void HandleMovement()
    {
        if (PlayerControl.Velocity.y > 0f)
        {
            DetectHorizontalMovement();
            ApplyGravity();
        }
        else
        {
            PlayerControl.PlayerMovementState = new PlayerMovementFallingState(PlayerControl, PlayerData);
            PlayerControl.PlayerMovementState.HandleMovement();
        }
    }

    public void DetectHorizontalMovement()
    {
        var horizontalInput = Input.GetAxisRaw("Horizontal");
        PlayerControl.Velocity.x = horizontalInput * PlayerData.MoveSpeed;
    }

    public void ApplyGravity()
    {
        var gravityScale = Input.GetButton("Jump") ? PlayerData.LowGravityScale : PlayerData.DefaultGravityScale;
        PlayerControl.Velocity.y += PlayerData.Gravity * gravityScale * Time.deltaTime;
    }

    public void Knockback(Vector2 knockbackDirection, float toSleep)
    {
        return;
    }

    public IEnumerator KnockbackCoroutine(Vector2 knockbackDirection, float toSleep)
    {
        return null;
    }

    public void BubbleJump()
    {
        PlayerControl.Velocity.y = PlayerData.JumpForce;
    }
}


