/// <summary>
/// Player's Movement State
/// </summary>
public interface IPlayerMovementState
{
    Player Player { get; }
    PlayerData PlayerData { get; }

    /// <summary>
    /// Handle Player's Movement
    /// </summary>
    public void HandleMovement();

    /// <summary>
    /// Handle Player's Animation
    /// </summary>
    public void HandleAnimation();
}
