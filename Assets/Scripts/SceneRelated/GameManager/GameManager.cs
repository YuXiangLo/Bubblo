using UnityEngine;
using UnityEngine.SceneManagement;
using System;

public class GameManager : MonoBehaviour
{
    public bool IsGameOver = false;
	public string CurrentLevel;
    public static GameManager Instance;
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
        SoundManager.ChangeBackgroundMusic(BackgroundMusicType.MainMenu);
		CurrentLevel = "Start";
        SceneManager.LoadScene(CurrentLevel);
    }

    public void StartGame()
    {
        // Load Scene1 and register callback for when it has loaded
        SceneManager.sceneLoaded += OnSceneLoaded;
        SoundManager.ChangeBackgroundMusic(BackgroundMusicType.InGame);
		CurrentLevel = SceneList.Scene[0];
        SceneManager.LoadScene(CurrentLevel);
    }

	public void GameOver()
	{
        SoundManager.ChangeBackgroundMusic(BackgroundMusicType.MainMenu);
		CurrentLevel = "Start";
		SceneManager.LoadScene(CurrentLevel);
	}

    public void GotoHomePage()
    {
        SoundManager.ChangeBackgroundMusic(BackgroundMusicType.MainMenu);
		CurrentLevel = "Start";
        SceneManager.LoadScene(CurrentLevel);
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
            SoundManager.ChangeBackgroundMusic(BackgroundMusicType.MainMenu);
			CurrentLevel = "Start";
            SceneManager.LoadScene(CurrentLevel);
            return;
        }
        else{
            string nextSceneName = SceneList.Scene[index + 1];
            SceneManager.sceneLoaded += OnSceneLoaded;
            SoundManager.ChangeBackgroundMusic(BackgroundMusicType.InGame);
			CurrentLevel = nextSceneName;
            SceneManager.LoadScene(CurrentLevel);
        }
    }

    public void LoadSpecificLevel(string levelName)
    {
        // Check if the level exists in the SceneList array
        if (Array.Exists(SceneList.Scene, scene => scene == levelName))
        {
            // Level exists, store current states and load the specified scene
            SceneManager.sceneLoaded += OnSceneLoaded;
            SoundManager.ChangeBackgroundMusic(BackgroundMusicType.InGame);
			CurrentLevel = levelName;
            SceneManager.LoadScene(CurrentLevel);
        }
        else
        {
            // Level doesn't exist, go back to the "Start" scene
            SoundManager.ChangeBackgroundMusic(BackgroundMusicType.MainMenu);
			CurrentLevel = "Start";
            SceneManager.LoadScene(CurrentLevel);
        }
    }

    /// <summary>
    /// Callback for when a scene has loaded    
    /// </summary>
    /// <param name="scene"></param>
    /// <param name="mode"></param>
    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }
}
