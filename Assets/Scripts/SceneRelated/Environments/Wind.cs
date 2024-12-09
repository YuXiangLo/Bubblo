using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wind : MonoBehaviour
{
    [SerializeField] private float BlowForceRatio = -0.6f;
    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            // Get the PlayerData component from the Player
            PlayerData playerData = collision.gameObject.GetComponent<PlayerData>();
			// Invert the gravity by multiplying by -1
			playerData.Gravity *= BlowForceRatio;
        }
    }

    void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            // Get the PlayerData component from the Player
            PlayerData playerData = collision.gameObject.GetComponent<PlayerData>();
			// Revert the gravity by multiplying by -1 again
			playerData.Gravity /= BlowForceRatio;
		}
	}
}
