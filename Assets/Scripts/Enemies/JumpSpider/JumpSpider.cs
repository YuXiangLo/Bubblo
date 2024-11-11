using UnityEngine;

namespace Enemies.JumpSpider
{
    public class JumpSpider : MonoBehaviour, IEnemyModifyHealth, IHealthPercentage
    {
        private JumpSpiderMovement SimpleSpiderMovement;
        private EnemyHealth SimpleSpiderHealth;

        public float HealthPercentage { get => SimpleSpiderHealth.HealthPercentage; }

        private void Awake()
        {
            SimpleSpiderMovement = GetComponent<JumpSpiderMovement>();
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
}