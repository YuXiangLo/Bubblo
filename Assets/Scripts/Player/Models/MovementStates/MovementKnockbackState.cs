using System.Collections;
using UnityEngine;

public class MovementKnockbackState : IMovementState
{
    private readonly Player Player;
    private readonly PlayerData Data;

    private readonly Vector2 KnockbackDirection;
    private float SleepTimer;

    public bool AttackEnabled => false;


    public MovementKnockbackState(Player player, PlayerData data, Vector2 knockbackDirection, float toSleep)
    {
        Player = player;
        Data = data;
        KnockbackDirection = knockbackDirection;
        SleepTimer = toSleep;
        Player.SetAnimation(AnimationStateType.Knockback);
    }

    public void Enter()
    {
        var velocity = Player.Velocity;
        velocity.x = (KnockbackDirection.x > 0) ? -Data.KnockbackForce : Data.KnockbackForce;
        velocity.y = Data.KnockbackTangent * Data.KnockbackForce;
        Player.Velocity = velocity;
    }

    public void Update()
    {
        SleepTimer -= Time.deltaTime;
        if (SleepTimer <= 0)
        {
            Player.ChangeMovementState(new MovementIdleState(Player, Data));
        }
        ApplyGravity();
    }

    private void ApplyGravity()
    {
        var velocity = Player.Velocity;
        velocity.y += Data.Gravity * Data.LowGravityScale * Time.deltaTime;
        Player.Velocity = velocity;
    }
}