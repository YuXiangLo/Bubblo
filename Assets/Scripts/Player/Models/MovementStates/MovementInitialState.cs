using UnityEngine;

public class MovementInitialState : IMovementState
{
    private readonly Player Player;
    private readonly PlayerData Data;

    public MovementInitialState(Player player, PlayerData data)
    {
        Player = player;
        Data = data;
    }

    public void Enter()
    {
        // Do nothing
    }

    public void Update()
    {
        if (Player.Grounded)
        {
            Player.ChangeMovementState(new MovementIdleState(Player, Data));
        }
        else
        {
            Player.ChangeMovementState(new MovementFallingState(Player, Data));
        }
    }
}