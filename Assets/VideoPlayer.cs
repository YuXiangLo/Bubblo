using UnityEngine;
using UnityEngine.Video;

public class VideoPlayback : MonoBehaviour
{
    public VideoPlayer videoPlayer;
    public string VideoFilename;

    void Start()
    {
        // Set the video file URL
        videoPlayer.url = System.IO.Path.Combine(Application.streamingAssetsPath, VideoFilename);
        
        // Prepare and play the video
        videoPlayer.Prepare();
        videoPlayer.Play();
        
        // Subscribe to the loopPointReached event
        videoPlayer.loopPointReached += OnVideoFinished;
    }

    void Update() {
        if (Input.anyKeyDown) {
            GameManager.Instance.SceneLevelup();
        }
    }

    // Callback method for when the video finishes playing
    private void OnVideoFinished(VideoPlayer source)
    {
        GameManager.Instance.SceneLevelup();
    }

    void OnDestroy()
    {
        // Unsubscribe from the event when the object is destroyed
        if (videoPlayer != null)
        {
            videoPlayer.loopPointReached -= OnVideoFinished;
        }
    }
}
