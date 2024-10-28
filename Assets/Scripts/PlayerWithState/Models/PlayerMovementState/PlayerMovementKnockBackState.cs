using System.Collections;
using UnityEngine;

public class PlayerMovementKnockBackState : IPlayerMovementState
{
    public PlayerControl PlayerControl { get; }
    public PlayerData PlayerData { get; }

    private readonly Vector2 KnockbackDirection;
    private readonly float ToSleep;

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
    
    private void Knockback(Vector2 knockbackDirection, float toSleep)
    {
        PlayerControl.StartCoroutine(KnockbackCoroutine(knockbackDirection, toSleep));
    }
    private IEnumerator KnockbackCoroutine(Vector2 knockbackDirection, float toSleep)
    {
        PlayerControl.Velocity.x = (knockbackDirection.x > 0) ? -PlayerData.KnockbackForce : PlayerData.KnockbackForce;
		PlayerControl.Velocity.y = PlayerData.KnockbackTangent * PlayerData.KnockbackForce;

		yield return new WaitForSeconds(toSleep);
        PlayerControl.ChangePlayerMovementState(new PlayerMovementInitialState(PlayerControl, PlayerData));
    }
    
}
