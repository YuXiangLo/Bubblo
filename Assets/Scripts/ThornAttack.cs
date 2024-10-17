using UnityEngine;

public class ThornAttack : MonoBehaviour
{
    [SerializeField] private float Damage = 1f;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        var colliderObject = collision.gameObject;
        if (colliderObject.CompareTag("Player"))
        {
            IModifyHealth player = colliderObject.GetComponent<IModifyHealth>();
            if (player != null)
            {
                player.TakeDamage(Damage);
            }
        }
    }
}
