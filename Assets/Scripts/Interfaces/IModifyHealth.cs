public interface IModifyHealth
{
    /// <summary>
    /// Method to reduce health by a specified amount
    /// </summary>
    public void TakeDamage(float amount);

    /// <summary>
    /// Method to increase health by a specified amount
    /// </summary>
    public void Heal(float amount);
}
