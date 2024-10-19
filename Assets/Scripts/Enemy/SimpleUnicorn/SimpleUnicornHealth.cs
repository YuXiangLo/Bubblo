using UnityEngine;

public class SimpleUnicornHealth : MonoBehaviour, IHealthPercentage, IModifyHealth
{
    [SerializeField] private float health = 100f;
    public float HealthPercentage { get => health / 100f; }

    public void TakeDamage(float amount)
    {
        health -= amount;
        if (health <= 0)
        {
            Die();
        }
    }

    public void Heal(float amount)
    {
        health += amount;
    }

    private void Die()
    {
        Debug.Log("Simple Unicorn has died!");
        Destroy(gameObject);
    }
}