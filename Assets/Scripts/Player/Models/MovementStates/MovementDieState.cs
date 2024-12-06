using System.Collections;
using UnityEngine;

public class MovementDieState : IMovementState
{
    private Player Player;
    private PlayerData PlayerData;

    public MovementDieState(Player player, PlayerData playerData)
    {
        Player = player;
        PlayerData = playerData;
        Player.SetAnimation(AnimationStateType.Die);
    }

    public void Enter()
    {
        Player.Velocity = Vector2.zero;
    }

    public void Update()
    {
        // Do nothing
    }

    private IEnumerator DieCoroutine()
    {
        var stateInfo = Player.Animator.GetCurrentAnimatorStateInfo(0);
        float remainingTime = stateInfo.length * (1f - stateInfo.normalizedTime);
        yield return new WaitForSeconds(remainingTime);
        //TODO: Add Game Over Logic
    }
}