using System;
using System.Collections;
using UnityEngine;

public class PlayerMovementInitialState: IPlayerMovementState
{
    public PlayerControl PlayerControl { get; }
    public PlayerData PlayerData { get; }

    public PlayerMovementInitialState(PlayerControl playerControl, PlayerData playerData)
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
        else 
        {
            PlayerControl.PlayerMovementState = new PlayerMovementFallingState(PlayerControl, PlayerData);
            PlayerControl.PlayerMovementState.HandleMovement();
        }
    }

    public void DetectHorizontalMovement()
    {
        // Should not be called
        return;
    }

    public void ApplyGravity()
    {
        // Should not be called
        return;
    }

    public void Knockback(Vector2 knockbackDirection, float toSleep)
    {
        // Should not be called
        return;
    }

    public IEnumerator KnockbackCoroutine(Vector2 knockbackDirection, float toSleep)
    {
        // Should not be called
        yield return null;
    }

    public void BubbleJump()
    {
        // Should not be called
        return;
    }
}


