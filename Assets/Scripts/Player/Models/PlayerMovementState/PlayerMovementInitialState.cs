using System;

public class PlayerMovementInitialState: IPlayerMovementState
{
    public Player Player { get; }
    public PlayerData PlayerData { get; }

    /// <summary>
    /// Constructor
    /// </summary>
    /// <param name="player">Player</param>
    /// <param name="playerData">PlayerData</param>
    public PlayerMovementInitialState(Player player, PlayerData playerData)
    {
        Player = player;
        PlayerData = playerData;
    }

    public void HandleMovement()
    {
        if (Player.IsGrounded)
        {
            Player.ChangePlayerMovementState(new PlayerMovementIdleState(Player, PlayerData));
        }
        else 
        {
            Player.ChangePlayerMovementState(new PlayerMovementFallingState(Player, PlayerData));
        }
    }

    public void HandleAnimation()
    {
        
    }
}


