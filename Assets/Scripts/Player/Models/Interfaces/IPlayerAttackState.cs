/// <summary>
/// Player's Attack State
/// </summary>
public interface IPlayerAttackState
{
    Player Player { get; }
    PlayerData PlayerData { get; }

    /// <summary>
    /// If the Attack State Has Set Animation
    /// </summary>
    bool ShouldShowAnimation { get; }

    /// <summary>
    /// Handle Player's Attack
    /// </summary>
    public void HandleAttack();

    /// <summary>
    /// Handle Player Being Knocked Back
    /// </summary>
    public void HandleKnockedBack();
}
