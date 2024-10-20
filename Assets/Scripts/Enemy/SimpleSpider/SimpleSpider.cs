using UnityEngine;

public class SimpleSpider : MonoBehaviour, IModifyHealth, IHealthPercentage
{
    private SimpleSpiderMovement SimpleSpiderMovement;
    private EnemyHealth SimpleSpiderHealth;

    public float HealthPercentage { get => SimpleSpiderHealth.HealthPercentage; }

    private void Awake()
    {
        SimpleSpiderMovement = GetComponent<SimpleSpiderMovement>();
        SimpleSpiderHealth = GetComponent<EnemyHealth>();
    }

    private void Update()
    {
        if (SimpleSpiderMovement.enabled)
            SimpleSpiderMovement.HandleMovement();
    }

    public void Heal(float amount)
    {
        SimpleSpiderHealth.Heal(amount);
    }

    public void TakeDamage(float amount)
    {
        SimpleSpiderHealth.TakeDamage(amount);
    }
}