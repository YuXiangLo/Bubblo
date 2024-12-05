using System.Runtime.CompilerServices;
using UnityEngine;

public class Heart : MonoBehaviour
{
    [SerializeField] private float HealAmount = 20f;
    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Player player = collision.gameObject.GetComponent<Player>();
            player.Heal(HealAmount);
            Destroy(gameObject);
        }
    }
}
