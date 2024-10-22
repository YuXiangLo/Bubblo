using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public LevelData LevelData; // LevelData
    public Transform Player;  // Reference to the player's transform
    public Vector3 Offset;    // Offset between the camera and the player (x and y offset)
    public float SmoothSpeed = 0.125f;  // How smoothly the camera follows the player
    
    private bool ShouldStopFollowing = false;

    void FixedUpdate()
    {
        ShouldStopFollowing = Player.position.y < LevelData.MinYAxisPosition + LevelData.StopFollowingXAxisYOffset;
        
        if (!ShouldStopFollowing) {
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
