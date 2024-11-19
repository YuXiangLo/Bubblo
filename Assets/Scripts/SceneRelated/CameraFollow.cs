// using UnityEngine;

// public class CameraFollow : MonoBehaviour
// {
//     public LevelData LevelData; // LevelData
//     private Transform Player;  // Reference to the player's transform
//     public Vector3 Offset;    // Offset between the camera and the player (x and y offset)
//     public float SmoothSpeed = 0.125f;  // How smoothly the camera follows the player
    
//     private bool ShouldStopFollowing = false;

//     void Start()
//     {
//         Player = GameObject.FindGameObjectWithTag("Player").transform;
//     }

//     void FixedUpdate()
//     {
//         ShouldStopFollowing = Player.position.y < LevelData.MinYAxisPosition + LevelData.StopFollowingXAxisYOffset;
        
//         if (!ShouldStopFollowing) {
//             Vector3 desiredPosition = new(
//                 Mathf.Max(LevelData.MinXAxisPosition, Player.position.x) + Offset.x,
//                 Mathf.Max(LevelData.MinYAxisPosition, Player.position.y) + Offset.y,
//                 transform.position.z);

// 			desiredPosition.x = Mathf.Min(desiredPosition.x, LevelData.MaxXAxisPosition);
                
//             Vector3 smoothedPosition = Vector3.Lerp(
//                 transform.position, desiredPosition, SmoothSpeed);

//             transform.position = smoothedPosition;
//         }
//     }
// }

using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public LevelData LevelData; // LevelData
    private Transform Player;  // Reference to the player's transform
    public Vector3 Offset;    // Offset between the camera and the player (x and y offset)
    public float SmoothSpeed = 1f; // How smoothly the camera follows the player
    
    private bool ShouldStopFollowing = false;
    private bool IsInspecting = false; // Flag to check if the inspect mode is active
    private Camera MainCamera; // Reference to the main camera

    public float ExpandedCameraSize = 10f; // Camera size when zooming out
    private float OriginalCameraSize; // Original camera size
    public KeyCode InspectKey = KeyCode.Tab; // Key to activate the inspect mode

    void Start()
    {
        Player = GameObject.FindGameObjectWithTag("Player").transform;
        MainCamera = Camera.main;
        OriginalCameraSize = MainCamera.orthographicSize; // Store the original camera size
    }

    void FixedUpdate()
    {
        // Check if the inspect mode is activated
        if (Input.GetKey(InspectKey))
        {
            ActivateInspectMode();
        }
        else if (IsInspecting)
        {
            DeactivateInspectMode();
        }

        // If the camera should follow the player
        if (!ShouldStopFollowing && !IsInspecting)
        {
            ShouldStopFollowing = Player.position.y < LevelData.MinYAxisPosition + LevelData.StopFollowingXAxisYOffset;

            Vector3 desiredPosition = new(
                Mathf.Max(LevelData.MinXAxisPosition, Player.position.x) + Offset.x,
                Mathf.Max(LevelData.MinYAxisPosition, Player.position.y) + Offset.y,
                transform.position.z);

            desiredPosition.x = Mathf.Min(desiredPosition.x, LevelData.MaxXAxisPosition);

            Vector3 smoothedPosition = Vector3.Lerp(
                transform.position, desiredPosition, SmoothSpeed);

            transform.position = smoothedPosition;
        }
    }

    void ActivateInspectMode()
    {
        if (!IsInspecting)
        {
            IsInspecting = true;
            MainCamera.orthographicSize = ExpandedCameraSize; // Expand the camera's view
            // GameObject.FindWithTag("Player").GetComponent<Player>().enabled = false;
        }

        // Adjust camera position within bounds
        Vector3 currentCameraPosition = transform.position;
        currentCameraPosition.x = Mathf.Clamp(currentCameraPosition.x, LevelData.MinInspectXAxisPosition, LevelData.MaxInspectXAxisPosition);
        currentCameraPosition.y = Mathf.Clamp(currentCameraPosition.y, LevelData.MinInspectYAxisPosition, Mathf.Infinity);
        transform.position = currentCameraPosition;
    }

    void DeactivateInspectMode()
    {
        IsInspecting = false;
        MainCamera.orthographicSize = OriginalCameraSize; // Reset the camera's view
        // GameObject.FindWithTag("Player").GetComponent<Player>().enabled = true;
    }
}
