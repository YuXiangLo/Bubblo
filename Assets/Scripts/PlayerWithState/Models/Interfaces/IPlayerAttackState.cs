public interface IPlayerAttackState
{
    Player Player { get; }
    PlayerData PlayerData { get; }

    public void HandleAttack();
}
