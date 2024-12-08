using UnityEngine;

public class MovementRescueState : IMovementState
{
    private readonly Player Player;
    private readonly PlayerData PlayerData;

    private float RescueTimer;

    public MovementRescueState(Player player, PlayerData playerData)
    {
        Player = player;
        PlayerData = playerData;

        RescueTimer = PlayerData.CelebrateClip.length;
    }

    public void Enter()
    {
        Player.SetAnimation(AnimationStateType.Celebrate);
        // TODO: Can add jump force here
    }

    public void Update()
    {
        RescueTimer -= Time.deltaTime;
        if (RescueTimer <= 0)
        {
            Player.ChangeMovementState(new MovementInitialState(Player, PlayerData));
        }
    }
}