using System.Collections;
using UnityEngine;

public class PlayerMovementKnockBackState : IPlayerMovementState
{
    public Player Player { get; }
    public PlayerData PlayerData { get; }

    private readonly Vector2 KnockbackDirection;
    private readonly float ToSleep;
    
    /// <summary>
    /// Constructor
    /// </summary>
    /// <param name="player">Player</param>
    /// <param name="playerData">PlayerData</param>
    /// <param name="knockbackDirection">KnockBack Direction</param>
    /// <param name="toSleep">To Sleep Time</param>
    public PlayerMovementKnockBackState(Player player, PlayerData playerData, Vector2 knockbackDirection, float toSleep) 
    {
        Player = player;
        PlayerData = playerData;
        KnockbackDirection = knockbackDirection;
        ToSleep = toSleep;
        Knockback(KnockbackDirection, ToSleep);
    }

    public void HandleMovement()
    {
        ApplyGravity();
    }

    private void ApplyGravity()
    {
        Player.Velocity.y += PlayerData.Gravity * PlayerData.LowGravityScale * Time.deltaTime;
    }
    
    private void Knockback(Vector2 knockbackDirection, float toSleep)
    {
        Player.StartCoroutine(KnockbackCoroutine(knockbackDirection, toSleep));
    }
    private IEnumerator KnockbackCoroutine(Vector2 knockbackDirection, float toSleep)
    {
        Player.Velocity.x = (knockbackDirection.x > 0) ? -PlayerData.KnockbackForce : PlayerData.KnockbackForce;
		Player.Velocity.y = PlayerData.KnockbackTangent * PlayerData.KnockbackForce;

		yield return new WaitForSeconds(toSleep);
        Player.ChangePlayerMovementState(new PlayerMovementInitialState(Player, PlayerData));
    }

	public void HandleAnimation()
	{
		Player.Animator.SetInteger("PlayerState", (int)PlayerState.PlayerStateType.Fall);
	}
}
