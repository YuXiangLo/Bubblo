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
        
    }

    public void DetectHorizontalMovement()
    {

    }

    public void ApplyGravity()
    {

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


