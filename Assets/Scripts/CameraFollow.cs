using UnityEngine;
using System;       

public class CameraFollow : MonoBehaviour
{
    public Transform player;  // Reference to the player's transform
    public Vector3 offset;    // Offset between the camera and the player (x and y offset)
    public float smoothSpeed = 0.125f;  // How smoothly the camera follows the player

    void FixedUpdate()
    {
        // Calculate the desired position based on the player's x and y coordinates, keeping the camera's z fixed
        Vector3 desiredPosition = new Vector3(Math.Max(0, player.position.x) + offset.x, transform.position.y, transform.position.z);

        // Smoothly interpolate between the camera's current position and the desired position
        Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);

        // Update the camera's position
        transform.position = smoothedPosition;
    }
}
