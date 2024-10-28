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
            PlayerControl.PlayerMovementState = new PlayerMovementFallingState(PlayerControl, PlayerData);
            PlayerControl.PlayerMovementState.HandleMovement();
        }
        else if (Input.GetButton("Jump"))
        {
            DetectHorizontalMovement();
            ApplyGravity();
        }
    }

    public void DetectHorizontalMovement()
    {
        PlayerControl.Velocity.x *= PlayerData.FloatingRatio;
    }

    public void ApplyGravity()
    {
        PlayerControl.Velocity.y = Mathf.Max(
            PlayerControl.Velocity.y + PlayerData.Gravity * PlayerData.LowGravityScale * Time.deltaTime,
            PlayerData.FloatingYSpeed);
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

    }
}

