using UnityEngine;

public class SimpleBee : MonoBehaviour, IModifyHealth, IHealthPercentage
{
    private SimpleBeeMovement SimpleBeeMovement;
    private EnemyHealth SimpleBeeHealth;

    public float HealthPercentage { get => SimpleBeeHealth.HealthPercentage; }

    private void Awake()
    {
        SimpleBeeMovement = GetComponent<SimpleBeeMovement>();
        SimpleBeeHealth = GetComponent<EnemyHealth>();
    }

    private void Update()
    {
        if (SimpleBeeMovement.enabled)
            SimpleBeeMovement.HandleMovement();
    }

    public void Heal(float amount)
    {
        SimpleBeeHealth.Heal(amount);
    }

    public void TakeDamage(float amount)
    {
        SimpleBeeHealth.TakeDamage(amount);
    }
}