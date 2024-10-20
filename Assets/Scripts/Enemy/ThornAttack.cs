using UnityEngine;

public class ThornAttack : MonoBehaviour
{
    [SerializeField] private float Damage = 1f;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log("Collision detected with " + collision.gameObject.name);
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
