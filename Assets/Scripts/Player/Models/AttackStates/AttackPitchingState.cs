using UnityEngine;

public class AttackPitchingState : IAttackState
{
    private Player Player;
    private PlayerData PlayerData;
    private Bubble Bubble;
    public bool LockAnimation => true;

    private float PitchTimer = 0f;

    private bool exit = false;
    public AttackPitchingState(Player player, PlayerData playerData, Bubble bubble)
    {
        Player = player;
        PlayerData = playerData;
        Bubble = bubble;
    }

    public void Enter()
    {
        Bubble.UpdateHoldingPosition();
        Bubble.Release();
        SoundManager.PlaySound(SoundType.Player, (int)PlayerSoundType.ThrowBubble);
        Player.SetAnimation(AnimationStateType.Pitching);
        PitchTimer = PlayerData.PitchClip.length;
    }

    public void Knockbacked()
    {
        Player.ChangeAttackState(new AttackIdleState(Player, PlayerData));
    }

    public void Update()
    {
        if (exit)
        {
            return;
        }
        PitchTimer -= Time.deltaTime;
        if (PitchTimer <= 0)
        {
            exit = true;
            Player.ChangeAttackState(new AttackIdleState(Player, PlayerData));
            Player.ChangeMovementState(new MovementInitialState(Player, PlayerData));
        }
    }
}
