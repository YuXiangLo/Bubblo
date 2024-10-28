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
            PlayerControl.ChangePlayerMovementState(new PlayerMovementGroundState(PlayerControl, PlayerData));
        }
        else 
        {
            PlayerControl.ChangePlayerMovementState(new PlayerMovementFallingState(PlayerControl, PlayerData));
        }
    }

}


