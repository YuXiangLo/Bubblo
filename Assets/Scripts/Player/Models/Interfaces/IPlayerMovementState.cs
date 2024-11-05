/// <summary>
/// Player's Movement State
/// </summary>
public interface IPlayerMovementState
{
    Player Player { get; }
    PlayerData PlayerData { get; }

    /// <summary>
    /// Handle Player's Movement;
    /// </summary>
    public void HandleMovement();
	public void HandleAnimation();
}
