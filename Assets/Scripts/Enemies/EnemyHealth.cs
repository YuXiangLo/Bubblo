using UnityEngine;

namespace Enemies
{
    public class EnemyHealth : MonoBehaviour, IHealthPercentage, IModifyHealth
    {
        [SerializeField] private float maxHealth = 100f;
        [SerializeField] private float health;
        public float HealthPercentage { get => health / 100f; }

        private void Start()
        {
            health = maxHealth;
        }

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
            Destroy(gameObject);
        }
    }
}
