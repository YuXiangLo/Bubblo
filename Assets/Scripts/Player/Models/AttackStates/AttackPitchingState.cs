using UnityEngine;

public class AttackPitchingState : IAttackState
{
    private Player Player;
    private PlayerData PlayerData;
    private Bubble Bubble;
    public bool LockAnimation => true;

    private float PitchTimer = 0f;

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

        var stateInfo = Player.Animator.GetCurrentAnimatorStateInfo(0);
        PitchTimer = stateInfo.length * (1f - stateInfo.normalizedTime);
    }

    public void Knockbacked()
    {
        Player.ChangeAttackState(new AttackIdleState(Player, PlayerData));
    }

    public void Update()
    {
        PitchTimer -= Time.deltaTime;
        if (PitchTimer <= 0)
        {
            Player.ChangeAttackState(new AttackIdleState(Player, PlayerData));
            Player.ChangeMovementState(new MovementInitialState(Player, PlayerData));
        }
    }
}
