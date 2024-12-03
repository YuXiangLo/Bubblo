using UnityEngine;

public class ParallaxScrolling : MonoBehaviour
{
    public Transform cloudLayer;    // Transform of the cloud layer
    public Transform mountainLayer; // Transform of the mountain layer
    public float cloudMultiplier = 0.2f;    // Speed multiplier for clouds
    public float mountainMultiplier = 0.5f; // Speed multiplier for mountains
    public float cloudYOffset = 5f;        // Offset for the cloud layer
    public float mountainYOffset = -2f;    // Offset for the mountain layer

    private Transform player;       // Reference to the player object
    private Transform cameraTransform; // Reference to the camera
    private Vector3 previousPlayerPosition; // Previous frame's player position

    void Start()
    {
        // Find the player object by tag
        player = GameObject.FindGameObjectWithTag("Player").transform;

        // Find the camera object by tag "MainCamera"
        cameraTransform = Camera.main.transform;

        // Initialize the previous player position
        if (player != null)
        {
            previousPlayerPosition = player.position;
        }
        else
        {
            Debug.LogError("Player object not found!");
        }

        if (cameraTransform == null)
        {
            Debug.LogError("Main Camera not found!");
        }
    }

    void Update()
    {
        if (player == null || cameraTransform == null) return;

        // Calculate the delta movement of the player
        Vector3 playerDelta = player.position - previousPlayerPosition;

        // Apply parallax effect to layers
        cloudLayer.position += new Vector3(playerDelta.x * cloudMultiplier, 0, 0);
        mountainLayer.position += new Vector3(playerDelta.x * mountainMultiplier, 0, 0);

        // Adjust layers' Y position based on camera's Y position
        cloudLayer.position = new Vector3(cloudLayer.position.x, cameraTransform.position.y + cloudYOffset, cloudLayer.position.z);
        mountainLayer.position = new Vector3(mountainLayer.position.x, cameraTransform.position.y + mountainYOffset, mountainLayer.position.z);

        // Update the previous player position
        previousPlayerPosition = player.position;

        // Optional: Loop the layers if necessary
        RepeatLayer(cloudLayer, 800f);  // Assuming 800 is the width of the cloud layer
        RepeatLayer(mountainLayer, 1200f); // Assuming 1200 is the width of the mountain layer
    }

    void RepeatLayer(Transform layer, float layerWidth)
    {
        if (layer.position.x < -layerWidth)
        {
            layer.position += new Vector3(layerWidth, 0, 0);
        }
        else if (layer.position.x > layerWidth)
        {
            layer.position -= new Vector3(layerWidth, 0, 0);
        }
    }
}
