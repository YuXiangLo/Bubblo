using UnityEngine;

public class MovementAchieveState : IMovementState
{
    private readonly Player Player;
    private readonly PlayerData PlayerData;

    private bool CompletedCelebration = false;
    private float CelebrateTimer;
    private float AchieveTimer;

    public MovementAchieveState(Player player, PlayerData playerData)
    {
        Player = player;
        PlayerData = playerData;

        CelebrateTimer = PlayerData.CelebrateClip.length;
        AchieveTimer = PlayerData.AchieveFloatClip.length;
    }

    public void Enter()
    {
        Player.SetAnimation(AnimationStateType.Celebrate);
        // TODO: Can add jump force here
    }

    public void Update()
    {
        if (!CompletedCelebration)
        {
            CelebrateTimer -= Time.deltaTime;
            if (CelebrateTimer <= 0)
            {
                CompletedCelebration = true;
                Player.SetAnimation(AnimationStateType.AchieveFloat);
                // TODO: Add physics here
            }
        }
        else
        {
            AchieveTimer -= Time.deltaTime;
            if (AchieveTimer <= 0)
            {
                Player.ChangeMovementState(new MovementInitialState(Player, PlayerData));
            }
        }
    }
}