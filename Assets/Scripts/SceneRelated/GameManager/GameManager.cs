using UnityEngine;
using UnityEngine.SceneManagement;
using System;

public class GameManager : MonoBehaviour
{
    public bool IsGameOver = false;
    public static GameManager Instance;
    private Player Player;
    private float CurrentHealthTemp;
    private float CurrentMagicPointTemp;
    [SerializeField] private SceneList SceneList;
    public PlayerHealth PlayerHealth;
    private MusicManager MusicManager;

    void Awake()
    {
        if (Instance == null) {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        } else {
            Destroy(gameObject);
        }

        MusicManager = MusicManager.Instance;
    }

    private void Start()
    {
        LoadScene("Start");
    }

    public void StartGame()
    {
        Debug.Log("Game Started");

        // Load Scene1 and register callback for when it has loaded
        SceneManager.sceneLoaded += OnSceneLoaded;
        LoadScene(SceneList.Scene[0]);
    }

	public void GameOver()
	{
		LoadScene("Start");
	}

    public void GotoHomePage()
    {
        LoadScene("Start");
    }

    /// <summary>
    /// Transition to the next scene
    /// </summary>
    public void SceneLevelup()
    {
        string currentSceneName = SceneManager.GetActiveScene().name;
        int index = Array.IndexOf(SceneList.Scene, currentSceneName);
        if (index == SceneList.Scene.Length - 1)
        {
            LoadScene("Start");
            return;
        }
        else{
            StoreCurrentStates();
            string nextSceneName = SceneList.Scene[index + 1];
            SceneManager.sceneLoaded += OnSceneLoaded;
            LoadScene(nextSceneName);
        }
    }

    public void LoadSpecificLevel(string levelName)
    {
        // Check if the level exists in the SceneList array
        if (Array.Exists(SceneList.Scene, scene => scene == levelName))
        {
            // Level exists, store current states and load the specified scene
            StoreCurrentStates();
            SceneManager.sceneLoaded += OnSceneLoaded;
            LoadScene(levelName);
        }
        else
        {
            // Level doesn't exist, go back to the "Start" scene
            LoadScene("Start");
        }
    }

    private void LoadScene(string sceneName)
    {
        if (sceneName == "Start")
        {
            MusicManager.PlayMainMenuBackgroundMusic();
        }
        else
        {
            MusicManager.PlayInGameBackgroundMusic();
        }

        SceneManager.LoadScene(sceneName);
    }

    private void StoreCurrentStates()
    {
        CurrentHealthTemp = Player.CurrentHealth;
        CurrentMagicPointTemp = Player.CurrentMagicPoint;
    }

    /// <summary>
    /// Callback for when a scene has loaded    
    /// </summary>
    /// <param name="scene"></param>
    /// <param name="mode"></param>
    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if (scene.name == SceneList.Scene[0])
        {
            Player = GameObject.FindWithTag("Player").GetComponent<Player>();
            Player.CurrentHealth = PlayerHealth.MaxHealth;
            SceneManager.sceneLoaded -= OnSceneLoaded;
        }
        else
        {
            GameObject playerObj = GameObject.FindWithTag("Player");
            Player = playerObj.GetComponent<Player>();
            // Restore health and magic points
            if (Player != null)
            {
                Player.CurrentHealth = CurrentHealthTemp;
                Player.CurrentMagicPoint = CurrentMagicPointTemp;
            }
            SceneManager.sceneLoaded -= OnSceneLoaded;
        }
    }
}
