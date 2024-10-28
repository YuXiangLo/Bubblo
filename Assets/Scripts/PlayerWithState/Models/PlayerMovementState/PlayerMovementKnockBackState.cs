using System.Collections;
using UnityEngine;

public class PlayerMovementKnockBackState : IPlayerMovementState
{
    public PlayerControl PlayerControl { get; }
    public PlayerData PlayerData { get; }

    private Vector2 KnockbackDirection;
    private float ToSleep;

    public PlayerMovementKnockBackState(PlayerControl playerControl, PlayerData playerData, Vector2 knockbackDirection, float toSleep) 
    {
        PlayerControl = playerControl;
        PlayerData = playerData;
        KnockbackDirection = knockbackDirection;
        ToSleep = toSleep;
    }

    public void HandleMovement()
    {
        Knockback(KnockbackDirection, ToSleep);
    }
    public void DetectHorizontalMovement()
    {

    }
    public void ApplyGravity()
    {

    }
    public void Knockback(Vector2 knockbackDirection, float toSleep)
    {
        PlayerControl.StartCoroutine(KnockbackCoroutine(knockbackDirection, toSleep));
    }
    public IEnumerator KnockbackCoroutine(Vector2 knockbackDirection, float toSleep)
    {
        PlayerControl.Velocity.x = (knockbackDirection.x > 0) ? -PlayerData.KnockbackForce : PlayerData.KnockbackForce;
		PlayerControl.Velocity.y = PlayerData.KnockbackTangent * PlayerData.KnockbackForce;

		yield return new WaitForSeconds(toSleep);
        PlayerControl.PlayerMovementState = new PlayerMovementInitialState(PlayerControl, PlayerData);
        PlayerControl.PlayerMovementState.HandleMovement();
    }
    public void BubbleJump()
    {
        return;
    }
}
