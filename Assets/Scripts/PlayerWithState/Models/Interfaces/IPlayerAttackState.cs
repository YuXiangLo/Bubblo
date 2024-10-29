public interface IPlayerAttackState
{
    PlayerControl PlayerControl { get; }
    PlayerData PlayerData { get; }

    public void HandleAttack();
}
