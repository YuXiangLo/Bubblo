using UnityEngine;
using UnityEngine.UI;

public class SliderValueProvider : MonoBehaviour
{
    private Slider slider;

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

    private void OnSliderValueChanged(float value)
    {
        // Notify SoundManager of the new volume
        if (SoundManager.Instance != null)
        {
            SoundManager.Instance.Volume = value;
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
