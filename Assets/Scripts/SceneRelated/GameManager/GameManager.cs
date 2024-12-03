using UnityEngine;
using UnityEngine.SceneManagement;
using System;

public class GameManager : MonoBehaviour
{
    public bool IsGameOver = false;
    public static GameManager Instance;
    private IPlayerStatus Player;
    private PlayerDataStatus StoredPlayerData;
    [SerializeField] private SceneList SceneList;

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

    public void StartGame()
    {
        // Load Scene1 and register callback for when it has loaded
        SceneManager.sceneLoaded += OnSceneLoaded;
        SceneManager.LoadScene(SceneList.Scene[0]);
    }

	public void GameOver()
	{
		SceneManager.LoadScene("Start");
	}

    public void GotoHomePage()
    {
        SceneManager.LoadScene("Start");
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
            SceneManager.LoadScene("Start");
            return;
        }
        else{
            StoreCurrentStates();
            string nextSceneName = SceneList.Scene[index + 1];
            SceneManager.sceneLoaded += OnSceneLoaded;
            SceneManager.LoadScene(nextSceneName);
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
            SceneManager.LoadScene(levelName);
        }
        else
        {
            // Level doesn't exist, go back to the "Start" scene
            SceneManager.LoadScene("Start");
        }
    }


    private void StoreCurrentStates()
    {
        StoredPlayerData = Player.GetPlayerStatus();
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
            Player = GameObject.FindWithTag("Player").GetComponent<IPlayerStatus>();
            Player.Initialize();
            SceneManager.sceneLoaded -= OnSceneLoaded;
        }
        else
        {
            Player = GameObject.FindWithTag("Player").GetComponent<IPlayerStatus>();
            // Restore health and magic points
            if (Player != null)
            {
                Player.SetPlayerStatus(StoredPlayerData);
            }
            SceneManager.sceneLoaded -= OnSceneLoaded;
        }
    }
}
