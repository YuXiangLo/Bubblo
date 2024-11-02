using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class GameManager : MonoBehaviour
{
    public bool isGameOver = false;
    public static GameManager Instance;
    private Player Player;
    private float CurrentHealthTemp;
    private float CurrentMagicPointTemp;
    private Vector3 PlayerSpawnPoint = new(0, 0, 0);    
    [SerializeField] private float TransitionDelay = 1f;

    void Awake()
    {
        if (Instance == null) {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        } else {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        SceneManager.LoadScene("Start");
    }

    private void Update()
    {
        if (isGameOver)
        {
            SceneManager.LoadScene("GameOver");
        }       
    }

    public void StartGame()
    {
        Debug.Log("Game Started");

        // Load Scene1 and register callback for when it has loaded
        SceneManager.sceneLoaded += OnSceneLoaded;
        SceneManager.LoadScene("Scene1");
    }

    public void SceneLevelup()
    {
        string currentSceneName = SceneManager.GetActiveScene().name;
        if (currentSceneName.StartsWith("Scene"))
        {
            if (int.TryParse(currentSceneName.Substring(5), out int currentLevel))
            {
                string nextSceneName = $"Scene{currentLevel + 1}";
                Debug.Log($"Next scene: {nextSceneName}");

                // Save the player's health and magic points
                CurrentHealthTemp = Player.CurrentHealth;
                CurrentMagicPointTemp = Player.CurrentMagicPoint;
                Debug.Log($"CurrentHealthTemp: {CurrentHealthTemp}");
                Debug.Log($"CurrentMagicPointTemp: {CurrentMagicPointTemp}");

                // Load the next scene and register callback for when it has loaded
                SceneManager.sceneLoaded += OnSceneLoaded;
                StartCoroutine(TransitionAfterDelay(nextSceneName));
            }
        }
    }

    private IEnumerator TransitionAfterDelay(string sceneToLoad)
    {
        yield return new WaitForSeconds(TransitionDelay);
        SceneManager.LoadScene(sceneToLoad);
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        Debug.Log($"Scene loaded: {scene.name}");

        if (scene.name == "Scene1")
        {
            Player = GameObject.FindWithTag("Player").GetComponent<Player>();
            SceneManager.sceneLoaded -= OnSceneLoaded;
        }
        else if (scene.name.StartsWith("Scene"))
        {
            StartCoroutine(FindAndSetupPlayerInNewScene());
            SceneManager.sceneLoaded -= OnSceneLoaded;
        }
    }

    private IEnumerator FindAndSetupPlayerInNewScene()
    {
        yield return null; // Wait for one frame to ensure all objects are loaded

        // Find the existing player in the new scene
        GameObject playerObj = GameObject.FindWithTag("Player");
        if (playerObj != null)
        {
            Player = playerObj.GetComponent<Player>();

            // Restore health and magic points
            if (Player != null)
            {
                Player.CurrentHealth = CurrentHealthTemp;
                Player.CurrentMagicPoint = CurrentMagicPointTemp;
                Debug.Log($"Player Health Restored: {Player.CurrentHealth}");
                Debug.Log($"Player Magic Restored: {Player.CurrentMagicPoint}");
            }
            else
            {
                Debug.LogError("Player component not found on the existing Player object.");
            }
        }
        else
        {
            Debug.LogError("No player object with tag 'Player' found in the new scene.");
        }
    }
}
