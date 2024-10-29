public class PlayerAttackInitialState : IPlayerAttackState
{
    public PlayerControl PlayerControl { get; }
    public PlayerData PlayerData { get; }
    
    public PlayerAttackInitialState(PlayerControl playerControl, PlayerData playerData)
    {
        PlayerControl = playerControl;
        PlayerData = playerData;

        PlayerControl.CurrentMagicPoint = PlayerData.MaxMagicPoint;
    }

    public void HandleAttack()
    {
        PlayerControl.ChangePlayerAttackState(new PlayerAttackIdleState(PlayerControl, PlayerData));
    }
}
