using System.Collections;
using UnityEngine;

public class AttackPitchingState : IAttackState
{
    private Player Player;
    private PlayerData PlayerData;
    private Bubble Bubble;
    public bool LockAnimation => true;

    public AttackPitchingState(Player player, PlayerData playerData, Bubble bubble)
    {
        Player = player;
        PlayerData = playerData;
        Bubble = bubble;
    }

    public void Enter()
    {
        Player.StartCoroutine(PitchCoroutine());
        Bubble.Release();
    }

    public void Knockbacked()
    {
        Player.ChangeAttackState(new AttackIdleState(Player, PlayerData));
    }

    public void Update()
    {
        // Do nothing
    }

    private IEnumerator PitchCoroutine()
    {
        var stateInfo = Player.Animator.GetCurrentAnimatorStateInfo(0);
        // Calculate the remaining time of the animation
        float remainingTime = stateInfo.length * (1f - stateInfo.normalizedTime);

        // Wait for the remaining time
        yield return new WaitForSeconds(remainingTime);

        // After the animation is complete, change the player state
        Player.ChangeAttackState(new AttackIdleState(Player, PlayerData));
    }
}