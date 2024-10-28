public class PlayerMovementInitialState: IPlayerMovementState
{
    public PlayerControl PlayerControl { get; }
    public PlayerData PlayerData { get; }

    public PlayerMovementInitialState(PlayerControl playerControl, PlayerData playerData)
    {
        PlayerControl = playerControl;
        PlayerData = playerData;
    }

    public void HandleMovement()
    {
        if (PlayerControl.IsGrounded)
        {
            PlayerControl.PlayerMovementState = new PlayerMovementGroundState(PlayerControl, PlayerData);
            PlayerControl.PlayerMovementState.HandleMovement();
        }
        else 
        {
            PlayerControl.PlayerMovementState = new PlayerMovementFallingState(PlayerControl, PlayerData);
            PlayerControl.PlayerMovementState.HandleMovement();
        }
    }

}


