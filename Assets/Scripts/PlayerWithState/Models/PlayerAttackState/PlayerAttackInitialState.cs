public class PlayerAttackInitialState : IPlayerAttackState
{
    public Player Player { get; }
    public PlayerData PlayerData { get; }
    
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
}
