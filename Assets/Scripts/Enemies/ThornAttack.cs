using UnityEngine;

namespace Enemies
{
    public class ThornAttack : MonoBehaviour
    {
        [SerializeField] private float Damage = 1f;
        [SerializeField] private float ToSleep = 0.3f;

        private void OnCollisionEnter2D(Collision2D collision)
        {
            var colliderObject = collision.gameObject;
            if (colliderObject.CompareTag("Player"))
            {
                if (colliderObject.TryGetComponent<IModifyHealth>(out var playerHealth))
                {
                    playerHealth.TakeDamage(Damage);
                }
                if (colliderObject.TryGetComponent<IKnockback>(out var playerKnockback))
                {
                    Vector2 knockbackDirection = collision.GetContact(0).normal;
                    playerKnockback.Knockback(knockbackDirection, ToSleep);
                }
            }
        }
    }
}

