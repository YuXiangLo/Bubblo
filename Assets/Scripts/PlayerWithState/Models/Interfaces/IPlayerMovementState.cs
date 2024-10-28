using UnityEngine;
using System.Collections;

public interface IPlayerMovementState
{
    PlayerControl PlayerControl { get; }
    PlayerData PlayerData { get; }

    public void HandleMovement();
    public void DetectHorizontalMovement();
    public void ApplyGravity();
    public void Knockback(Vector2 knockbackDirection, float toSleep);
    public IEnumerator KnockbackCoroutine(Vector2 knockbackDirection, float toSleep);
    public void BubbleJump();
}
