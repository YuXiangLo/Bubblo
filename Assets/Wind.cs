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
            if (playerData != null)
            {
                // Invert the gravity by multiplying by -1
                playerData.Gravity *= BlowForceRatio;
                Debug.Log("Gravity inverted! New gravity: " + playerData.Gravity);
            }
            else
            {
                Debug.LogError("PlayerData component not found on the Player object.");
            }
        }
    }

    void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            // Get the PlayerData component from the Player
            PlayerData playerData = collision.gameObject.GetComponent<PlayerData>();
            if (playerData != null)
            {
                // Revert the gravity by multiplying by -1 again
                playerData.Gravity /= BlowForceRatio;
                Debug.Log("Gravity reverted! New gravity: " + playerData.Gravity);
            }
            else
            {
                Debug.LogError("PlayerData component not found on the Player object.");
            }
        }
    }
}
