using UnityEngine;

public class AnimationController : MonoBehaviour
{
    private Animator animator;
    private RectTransform rectTransform;

    [Header("Vertical Floating Settings")]
    public float amplitude = 50f; // Height of the floating effect
    public float frequency = 1f;  // Speed of the floating effect

    [Header("Wind Effect Settings")]
    public float windStrength = 20f; // Horizontal wind strength
    public float noiseSpeed = 0.5f;    // Speed of wind changes

    private Vector3 startPosition;

    void Start()
    {
        rectTransform = GetComponent<RectTransform>();
        startPosition = rectTransform.anchoredPosition;
    }

    void Update()
    {
        // Irregular vertical motion
        float verticalOffset = Mathf.Sin(Time.time * frequency) * amplitude;

        // Simulating wind (random horizontal offsets using Perlin Noise)
        float windOffset = (Mathf.PerlinNoise(Time.time * noiseSpeed, 0f) - 0.5f) * 2 * windStrength;

        // Update position with irregular floating
        rectTransform.anchoredPosition = new Vector3(startPosition.x + windOffset, startPosition.y + verticalOffset, startPosition.z);
    }
}
