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
        SoundManager.PlaySound(SoundType.Player, (int)PlayerSoundType.ThrowBubble);
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
        yield return new WaitForSeconds(0.2f);
        Player.ChangeAttackState(new AttackIdleState(Player, PlayerData));
        Player.ChangeMovementState(new MovementInitialState(Player, PlayerData));
    }
}
