using UnityEngine;

namespace Enemies.AssaultBee
{
    public class AssaultBee : MonoBehaviour, IEnemyModifyHealth
    {
        private AssaultBeeMovement Movement;
        private EnemyHealth Health;

        private void Awake()
        {
            Movement = GetComponent<AssaultBeeMovement>();
            Health = GetComponent<EnemyHealth>();
        }

        private void Update()
        {
            if (Movement.enabled)
                Movement.HandleMovement();
        }

        public void Heal(float amount)
        {
            Health.Heal(amount);
        }

        public void TakeDamage(float amount)
        {
            Health.TakeDamage(amount);
        }
    }
}