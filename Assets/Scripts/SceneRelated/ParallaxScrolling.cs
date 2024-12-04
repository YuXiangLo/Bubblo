using UnityEngine;

public class ParallaxScrolling : MonoBehaviour
{
    public Transform cloudLayer;    // Transform of the cloud layer
    public Transform mountainLayer; // Transform of the mountain layer
    public float cloudMultiplier = 0.9f;    // Speed multiplier for clouds
    public float mountainMultiplier = 0.85f; // Speed multiplier for mountains
    public float cloudYOffset = 0f;        // Offset for the cloud layer
    public float mountainYOffset = 0f;    // Offset for the mountain layer

    private Transform cameraTransform; // Reference to the camera
    private Vector3 previousCameraPosition; // Previous frame's camera position

    void Start()
    {
        // Find the camera object by tag "MainCamera"
        cameraTransform = Camera.main.transform;

        // Initialize the previous camera position
        if (cameraTransform != null)
        {
            previousCameraPosition = cameraTransform.position;
        }
        else
        {
            Debug.LogError("Main Camera not found!");
        }
    }

    void Update()
    {
        if (cameraTransform == null) return;

        // Calculate the delta movement of the camera
        Vector3 cameraDelta = cameraTransform.position - previousCameraPosition;

        // Apply parallax effect to layers based on camera's horizontal movement
        cloudLayer.position += new Vector3(cameraDelta.x * cloudMultiplier, 0, 0);
        mountainLayer.position += new Vector3(cameraDelta.x * mountainMultiplier, 0, 0);

        // Adjust layers' Y position to follow the camera's vertical position
        cloudLayer.position = new Vector3(cloudLayer.position.x, cameraTransform.position.y + cloudYOffset, cloudLayer.position.z);
        mountainLayer.position = new Vector3(mountainLayer.position.x, cameraTransform.position.y + mountainYOffset, mountainLayer.position.z);

        // Update the previous camera position
        previousCameraPosition = cameraTransform.position;
    }
}
