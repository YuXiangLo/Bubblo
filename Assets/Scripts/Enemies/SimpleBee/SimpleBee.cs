using UnityEngine;

namespace Enemyies.SimpleBee 
{
    public class SimpleBee : MonoBehaviour, IModifyHealth
    {
        private SimpleBeeMovement Movement;
        private EnemyHealth Health;

        private void Awake()
        {
            Movement = GetComponent<SimpleBeeMovement>();
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
