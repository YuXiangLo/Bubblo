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
        Update();
    }

    public void Update()
    {
        if (Player.Grounded)
        {
            Player.ChangeMovementState(new MovementIdleState(Player, Data));
        }
        else if (Player.Velocity.y > 0)
        {
            Player.ChangeMovementState(new MovementRisingState(Player, Data));
        }
        else
        {
            Player.ChangeMovementState(new MovementFallingState(Player, Data));
        }
    }
}