using UnityEngine;

public class BossUnicorn : MonoBehaviour
{
    private EnemyHealth Health;
    private BossUnicornMovement Movement;

    private void Start()
    {
        Health = GetComponent<EnemyHealth>();
        Movement = GetComponent<BossUnicornMovement>();
    }

    public void TakeDamage(float amount)
    {
        Health.TakeDamage(amount);
    }

    public void Heal(float amount)
    {
        Health.Heal(amount);
    }

    private void Update()
    {
        if (Movement.enabled)
            Movement.HandleMovement();
    }
}