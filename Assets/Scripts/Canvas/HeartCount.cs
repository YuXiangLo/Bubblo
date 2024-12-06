using UnityEngine;
using UnityEngine.UI;

public class CanvasHeartsUI : MonoBehaviour
{
    public GameObject heartPrefab; // Prefab for the heart UI element
    public RectTransform heartsParent; // Parent RectTransform for positioning hearts
    public Vector2 heartSpacing = new Vector2(100, 0); // Spacing between hearts (horizontal, vertical)
    private int currentLives; // Number of lives (hearts)
    public int maxLives = 5; // Maximum number of lives (hearts)
    private GameObject[] hearts; // Array to hold heart instances
    private IHealthPercentage Player;

    void Start()
    {
        Player = FindObjectOfType<Player>().GetComponent<IHealthPercentage>();
        UpdateUI();
    }

    private void Update() 
    {
        UpdateUI();
    }

    private void UpdateUI()
    {
        // Destroy all existing hearts
        if (hearts != null)
        {
            foreach (var heart in hearts)
            {
                if (heart != null)
                {
                    Destroy(heart);
                }
            }
        }

        currentLives = (int)(Player.HealthPercentage * maxLives);

        // Create a new array to hold heart instances
        hearts = new GameObject[currentLives];

        // Generate hearts and position them
        for (int i = 0; i < currentLives; i++)
        {
            // Instantiate a heart prefab as a child of heartsParent
            GameObject newHeart = Instantiate(heartPrefab, heartsParent);

            // Position the heart based on its index
            RectTransform heartRect = newHeart.GetComponent<RectTransform>();
            if (heartRect == null)
            {
                Debug.LogError("Heart prefab must have a RectTransform component.");
                continue;
            }

            // Set the position of the heart based on the index and spacing
            heartRect.anchoredPosition = heartsParent.anchoredPosition + new Vector2(i * heartSpacing.x, i * heartSpacing.y);

            // Store the heart instance
            hearts[i] = newHeart;
        }
    }
}