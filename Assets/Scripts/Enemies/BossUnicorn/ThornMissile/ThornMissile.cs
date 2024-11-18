using UnityEngine;

namespace Enemies.BossUnicorn
{
    public class ThornMissile : MonoBehaviour
    {
        [SerializeField] private float Damage = 15f;
        [SerializeField] private float ToSleep = 0.1f;
        private ThornMissileMovement ThornMissileMovement;

        private void Awake()
        {
            ThornMissileMovement = GetComponent<ThornMissileMovement>();
            ThornMissileMovement.SetDirection(GameObject.Find("Player").transform.position);
        }

        private void Update()
        {
            ThornMissileMovement.HandleMovement();
        }

        private void OnCollisionEnter2D(Collision2D other)
        {
            var colliderObject = other.gameObject;
            if (colliderObject.CompareTag("Player"))
            {
                if (colliderObject.TryGetComponent<IModifyHealth>(out var playerHealth))
                {
                    playerHealth.TakeDamage(Damage);
                }
                if (colliderObject.TryGetComponent<IKnockback>(out var playerKnockback))
                {
                    Vector2 knockbackDirection = other.GetContact(0).normal;
                    playerKnockback.Knockback(knockbackDirection, ToSleep);
                }
            }
            Destroy(gameObject);
        }
    }
}
