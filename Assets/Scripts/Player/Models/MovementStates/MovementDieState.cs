using UnityEngine;

public class MovementDieState : IMovementState
{
    private Player Player;
    private PlayerData PlayerData;

    public MovementDieState(Player player, PlayerData playerData)
    {
        Player = player;
        PlayerData = playerData;
    }

    public void Enter()
    {
        Debug.Log("Player is dead");
        Player.SetAnimation(AnimationStateType.Die);
        Player.Velocity = Vector2.zero;
    }

    public void Update()
    {
        
    }
}