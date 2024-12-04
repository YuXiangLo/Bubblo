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
        Bubble.Release();
        Player.SetAnimation(AnimationStateType.Pitching);
        Player.StartCoroutine(PitchCoroutine());
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
        float remainingTime = stateInfo.length * (1f - stateInfo.normalizedTime);
        yield return new WaitForSeconds(remainingTime);
        Player.ChangeAttackState(new AttackIdleState(Player, PlayerData));
    }
}