using UnityEngine;
using UnityEngine.UI;

public class SliderValueProvider : MonoBehaviour
{
    private Slider slider;
    private bool isMuted;
    [SerializeField]private Sprite mute;
    [SerializeField]private Sprite unmute;
    public Button button;

    void Awake()
    {
        // Get the Slider component
        slider = GetComponent<Slider>();
    }

    void Start()
    {
        // Initialize the slider value from SoundManager
        if (SoundManager.Instance != null)
        {
            slider.value = SoundManager.Instance.Volume;
        }

        // Add listener to update SoundManager when slider value changes
        slider.onValueChanged.AddListener(OnSliderValueChanged);
    }

    public void Mute()
    {
        isMuted = !isMuted;

        // Update SoundManager volume immediately
        if (SoundManager.Instance != null)
        {
            if (isMuted)
            {
                SoundManager.Instance.Volume = 0; // Set volume to 0 when muted
            }
            else
            {
                SoundManager.Instance.Volume = slider.value; // Restore volume to slider value
            }
        }
        button.image.sprite = isMuted ? mute : unmute;
    }

    private void OnSliderValueChanged(float value)
    {
        // Notify SoundManager of the new volume, considering mute state
        if (SoundManager.Instance != null)
        {
            SoundManager.Instance.Volume = isMuted ? 0 : value;
        }
    }

    void OnDestroy()
    {
        // Remove listener when the object is destroyed
        if (slider != null)
        {
            slider.onValueChanged.RemoveListener(OnSliderValueChanged);
        }
    }
}
