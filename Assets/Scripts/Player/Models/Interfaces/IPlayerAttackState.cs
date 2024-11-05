/// <summary>
/// Player's Attack State
/// </summary>
public interface IPlayerAttackState
{
    Player Player { get; }
    PlayerData PlayerData { get; }

    /// <summary>
    /// Handle Player's Attack
    /// </summary>
    public void HandleAttack();
	public void HandleAnimation();
}
