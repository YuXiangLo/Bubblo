public class PlayerMovementInitialState: IPlayerMovementState
{
    public Player Player { get; }
    public PlayerData PlayerData { get; }

    public PlayerMovementInitialState(Player player, PlayerData playerData)
    {
        Player = player;
        PlayerData = playerData;
    }

    public void HandleMovement()
    {
        if (Player.IsGrounded)
        {
            Player.ChangePlayerMovementState(new PlayerMovementGroundState(Player, PlayerData));
        }
        else 
        {
            Player.ChangePlayerMovementState(new PlayerMovementFallingState(Player, PlayerData));
        }
    }

}

