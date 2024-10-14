using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform Player;  // Reference to the player's transform
    public Vector3 Offset;    // Offset between the camera and the player (x and y offset)
    public float SmoothSpeed = 0.125f;  // How smoothly the camera follows the player

    void FixedUpdate()
    {
        Vector3 desiredPosition = new(Mathf.Max(0, Player.position.x) + Offset.x, transform.position.y, transform.position.z);

        Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, SmoothSpeed);

        transform.position = smoothedPosition;
    }
}
