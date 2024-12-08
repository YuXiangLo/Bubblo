using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public LevelData LevelData; // LevelData
    private Transform Player;  // Reference to the player's transform
    public Vector3 Offset;    // Offset between the camera and the player (x and y offset)
    public float SmoothSpeed = 0.125f;  // How smoothly the camera follows the player
    
    public bool EndLevel { get; set; }

    void Start()
    {
        Player = GameObject.FindGameObjectWithTag("Player").transform;
        EndLevel = false;
    }

    void FixedUpdate()
    {
        var playerOutOfBounds = Player.position.y < LevelData.MinYAxisPosition + LevelData.StopFollowingXAxisYOffset;
        var shouldStopFollowing = EndLevel || playerOutOfBounds;
        
        if (!shouldStopFollowing) {
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
}