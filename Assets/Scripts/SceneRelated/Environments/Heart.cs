using UnityEngine;

public class Heart : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Player player = collision.gameObject.GetComponent<Player>();
            player.Heal(100f);
            Debug.Log("Heal!");
            Destroy(gameObject);
        }
    }
}
