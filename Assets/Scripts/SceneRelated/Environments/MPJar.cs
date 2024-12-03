using UnityEngine;

public class MPJar : MonoBehaviour
{
    [SerializeField] private float MPAmount = 30f;
    void OnTriggerEnter2D(Collider2D collision) {
        if (collision.gameObject.CompareTag("Player")) {
            Player Player = collision.gameObject.GetComponent<Player>();
            Player.Recharge(MPAmount);
            Destroy(gameObject);
        }
    }
}
