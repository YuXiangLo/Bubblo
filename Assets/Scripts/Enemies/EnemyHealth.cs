using UnityEngine;
using System.Collections; // Required for IEnumerator

namespace Enemies
{
    public class EnemyHealth : MonoBehaviour, IHealthPercentage, IModifyHealth
    {
        [SerializeField] private float maxHealth = 100f;
        [SerializeField] private float health;
        [SerializeField] private float transparencyValue = 0.7f;
        [SerializeField] private float blipDuration = 0.1f; // Duration of the transparency effect
        private SpriteRenderer spriteRenderer; // Reference to the SpriteRenderer
        private Color originalColor; // Store the original color including alpha

        public float HealthPercentage { get => health / maxHealth; }

        private void Start()
        {
            health = maxHealth;

            // Get the SpriteRenderer and store the original color
            spriteRenderer = GetComponent<SpriteRenderer>();
            if (spriteRenderer != null)
            {
                originalColor = spriteRenderer.color;
            }
        }

        public void TakeDamage(float amount)
        {
            health -= amount;

            // Trigger the transparency effect
            if (spriteRenderer != null)
            {
                StartCoroutine(ShowTransparencyEffect());
            }

            if (health <= 0)
            {
                Die();
            }
        }

        public void Heal(float amount)
        {
            health += amount;
            if (health > maxHealth)
            {
                health = maxHealth;
            }
        }

        private void Die()
        {
            Destroy(gameObject);
        }

        private IEnumerator ShowTransparencyEffect()
        {
            if (spriteRenderer != null)
            {
                // Set the transparency to the target value
                Color newColor = spriteRenderer.color;
                newColor.a = transparencyValue; // Adjust alpha
                spriteRenderer.color = newColor;

                // Wait for the duration
                yield return new WaitForSeconds(blipDuration);

                // Revert to the original color
                spriteRenderer.color = originalColor;
            }
        }
    }
}
