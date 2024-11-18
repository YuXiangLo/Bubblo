using UnityEngine;

public class MPJar : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D collision) {
        if (collision.gameObject.CompareTag("Player")) {
            Player Player = collision.gameObject.GetComponent<Player>();
            Player.RestoreMP();
            Debug.Log("Restore MP!");
            Destroy(gameObject);
        }
    }
}
