using UnityEngine;

namespace Enemies.BossUnicorn
{
    public class BossUnicorn : MonoBehaviour
    {
        private EnemyHealth Health;
        private BossUnicornMovement Movement;
        private Animator animator;

        private void Start()
        {
            Health = GetComponent<EnemyHealth>();
            Movement = GetComponent<BossUnicornMovement>();
            animator = GetComponent<Animator>();
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
            animator.SetBool("IsSprinting", Movement.IsSprinting);
            animator.SetBool("IsAttacking", Movement.IsAttacking);
        }
    }
}
