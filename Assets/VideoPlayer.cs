using UnityEngine;
using UnityEngine.Video;

public class VideoSceneTransition : MonoBehaviour
{
    private VideoPlayer videoPlayer;

    void Start()
    {
        // Try to get the VideoPlayer component attached to this GameObject
        videoPlayer = GetComponent<VideoPlayer>();

        if (videoPlayer != null)
        {
            // Subscribe to the loopPointReached event
            videoPlayer.loopPointReached += OnVideoFinished;
        }
        else
        {
            Debug.LogError("VideoSceneTransition requires a VideoPlayer component!");
        }
    }

    private void OnVideoFinished(VideoPlayer vp)
    {
        // Call LevelUp when the video finishes playing
        GameManager.Instance.SceneLevelup();
    }

    private void OnDestroy()
    {
        if (videoPlayer != null)
        {
            // Clean up event subscription to avoid memory leaks
            videoPlayer.loopPointReached -= OnVideoFinished;
        }
    }
}
