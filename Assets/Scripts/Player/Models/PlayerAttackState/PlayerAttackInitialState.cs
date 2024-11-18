public class PlayerAttackInitialState : IPlayerAttackState
{
    public Player Player { get; }
    public PlayerData PlayerData { get; }
    public bool ShouldShowAnimation { get; } = false;
    
    /// <summary>
    /// Constructor
    /// </summary>
    /// <param name="player">Player</param>
    /// <param name="playerData">PlayerData</param>
    public PlayerAttackInitialState(Player player, PlayerData playerData)
    {
        Player = player;
        PlayerData = playerData;

        Player.CurrentMagicPoint = PlayerData.MaxMagicPoint;
    }

    public void HandleAttack()
    {
        Player.ChangePlayerAttackState(new PlayerAttackIdleState(Player, PlayerData));
    }

    public void HandleKnockedBack() 
    {
        return;    
    }
}
