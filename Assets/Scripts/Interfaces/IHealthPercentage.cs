/// <summary>
/// Interface for objects that have a current health value
/// </summary>
public interface IHealthPercentage
{
    /// <summary>
    /// Current health value
    /// </summary>
    public float HealthPercentage { get; }
}