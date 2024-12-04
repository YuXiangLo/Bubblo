using System.Collections;
using UnityEngine;

public class MovementKnockbackState : IMovementState
{
    private readonly Player Player;
    private readonly PlayerData Data;

    private readonly Vector2 KnockbackDirection;
    private readonly float ToSleep;

    public MovementKnockbackState(Player player, PlayerData data, Vector2 knockbackDirection, float toSleep)
    {
        Player = player;
        Data = data;
        KnockbackDirection = knockbackDirection;
        ToSleep = toSleep;
        Player.SetAnimation(AnimationStateType.Knockback);
    }

    public void Enter()
    {
        Update();
        Player.StartCoroutine(KnockbackCoroutine(KnockbackDirection, ToSleep));
    }

    public void Update()
    {
        ApplyGravity();
    }

    private IEnumerator KnockbackCoroutine(Vector2 knockbackDirection, float toSleep)
    {
        var velocity = Player.Velocity;
        velocity.x = (knockbackDirection.x > 0) ? -Data.KnockbackForce : Data.KnockbackForce;
        velocity.y = Data.KnockbackTangent * Data.KnockbackForce;
        Player.Velocity = velocity;

        yield return new WaitForSeconds(toSleep);
        Player.ChangeMovementState(new MovementInitialState(Player, Data));
    }

    private void ApplyGravity()
    {
        var velocity = Player.Velocity;
        velocity.y += Data.Gravity * Data.LowGravityScale * Time.deltaTime;
        Player.Velocity = velocity;
    }
}