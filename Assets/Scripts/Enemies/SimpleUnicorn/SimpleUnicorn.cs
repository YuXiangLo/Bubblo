using UnityEngine;

namespace SimpleUnicorn
{
    public class SimpleUnicorn : MonoBehaviour, IModifyHealth
    {
        private SimpleUnicornMovement SimpleUnicornMovement;
        private EnemyHealth SimpleUnicornHealth;

        public void TakeDamage(float amount)
        {
            SimpleUnicornHealth.TakeDamage(amount);
        }

        public void Heal(float amount)
        {
            SimpleUnicornHealth.Heal(amount);
        }

        private void Awake()
        {
            SimpleUnicornMovement = GetComponent<SimpleUnicornMovement>();
            SimpleUnicornHealth = GetComponent<EnemyHealth>();
        }

        private void Update()
        {
            SimpleUnicornMovement.HandleMovement();
        }
    }
}
