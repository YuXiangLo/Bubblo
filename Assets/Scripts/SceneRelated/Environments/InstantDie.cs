using UnityEngine;

public class DamageDealer : MonoBehaviour
{
    public float damageAmount = 100f;

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            PlayerHealth playerHealth = collision.gameObject.GetComponent<PlayerHealth>();
			playerHealth.TakeDamage(damageAmount);
        }
    }
}

