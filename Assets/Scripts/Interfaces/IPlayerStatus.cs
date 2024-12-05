public interface IPlayerStatus
{
    public void Initialize();

    public PlayerDataStatus GetPlayerStatus();

    public void SetPlayerStatus(PlayerDataStatus status);
}