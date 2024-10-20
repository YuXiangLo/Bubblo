using UnityEngine;

public class ThornAttack : MonoBehaviour
{
    [SerializeField] private float Damage = 1f;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        var colliderObject = collision.gameObject;
        if (colliderObject.CompareTag("Player"))
        {
            if (colliderObject.TryGetComponent<IModifyHealth>(out var player))
            {
                player.TakeDamage(Damage);
            }
        }
    }
}
