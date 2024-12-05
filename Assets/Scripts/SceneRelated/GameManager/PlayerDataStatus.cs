public class PlayerDataStatus
{
    public float Health { get; private set; }
    public float Magic { get; private set; }

    public PlayerDataStatus(float currentHealth, float currentMP)
    {
        Health = currentHealth;
        Magic = currentMP;
    }
}