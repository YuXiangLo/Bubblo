using System;
using System.Collections;
using UnityEngine;

public class PlayerMovementFallingState: IPlayerMovementState
{
    public PlayerControl PlayerControl { get; }
    public PlayerData PlayerData { get; }

    public PlayerMovementFallingState(PlayerControl playerControl, PlayerData playerData)
    {
        PlayerControl = playerControl;
        PlayerData = playerData;
    }

    public void HandleMovement()
    {
        if (PlayerControl.IsGrounded)
        {
            PlayerControl.PlayerMovementState = new PlayerMovementGroundState(PlayerControl, PlayerData);
            PlayerControl.PlayerMovementState.HandleMovement();
        }
        else if (PlayerControl.Velocity.y > 0f)
        {
            PlayerControl.PlayerMovementState = new PlayerMovementJumpingState(PlayerControl, PlayerData);
            PlayerControl.PlayerMovementState.HandleMovement();
        }
        else if (Input.GetButtonDown("Jump"))
        {
            PlayerControl.PlayerMovementState = new PlayerMovementFloatingState(PlayerControl, PlayerData);
            PlayerControl.PlayerMovementState.HandleMovement();
        }
        else 
        {
            DetectHorizontalMovement();
            ApplyGravity();
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
        
        // Limit Falling Speed
        PlayerControl.Velocity.y = Mathf.Max(PlayerControl.Velocity.y, PlayerData.MaxFallingSpeed);
    }

    public void Knockback(Vector2 knockbackDirection, float toSleep)
    {
        
    }

    public IEnumerator KnockbackCoroutine(Vector2 knockbackDirection, float toSleep)
    {
        yield return new Exception();
    }

    public void BubbleJump()
    {
        PlayerControl.Velocity.y = PlayerData.JumpForce;
    }
}


