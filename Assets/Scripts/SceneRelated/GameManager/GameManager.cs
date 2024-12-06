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
        SoundManager.ChangeBackgroundMusic(BackgroundMusicType.MainMenu);
        SceneManager.LoadScene("Start");
    }

    public void StartGame()
    {
        // Load Scene1 and register callback for when it has loaded
        SceneManager.sceneLoaded += OnSceneLoaded;
        SoundManager.ChangeBackgroundMusic(BackgroundMusicType.InGame);
        SceneManager.LoadScene(SceneList.Scene[0]);
    }

	public void GameOver()
	{
        SoundManager.ChangeBackgroundMusic(BackgroundMusicType.MainMenu);
		SceneManager.LoadScene("Start");
	}

    public void GotoHomePage()
    {
        SoundManager.ChangeBackgroundMusic(BackgroundMusicType.MainMenu);
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
            SoundManager.ChangeBackgroundMusic(BackgroundMusicType.MainMenu);
            SceneManager.LoadScene("Start");
            return;
        }
        else{
            StoreCurrentStates();
            string nextSceneName = SceneList.Scene[index + 1];
            SceneManager.sceneLoaded += OnSceneLoaded;
            SoundManager.ChangeBackgroundMusic(BackgroundMusicType.InGame);
            SceneManager.LoadScene(nextSceneName);
        }
    }

    public void LoadSpecificLevel(string levelName, bool isCalledByLevelSelect)
    {
        // Check if the level exists in the SceneList array
        if (Array.Exists(SceneList.Scene, scene => scene == levelName))
        {
            // Level exists, store current states and load the specified scene
            if (isCalledByLevelSelect)
                StoredPlayerData = new PlayerDataStatus(100, 10); // NOTE Yu Xiang: Forgive me for the magic number, this is Player.MaxHealth and MaxMagic
            else
                StoreCurrentStates();
            SceneManager.sceneLoaded += OnSceneLoaded;
            SoundManager.ChangeBackgroundMusic(BackgroundMusicType.InGame);
            SceneManager.LoadScene(levelName);
        }
        else
        {
            // Level doesn't exist, go back to the "Start" scene
            SoundManager.ChangeBackgroundMusic(BackgroundMusicType.MainMenu);
            SceneManager.LoadScene("Start");
        }
    }


    private void StoreCurrentStates()
    {
        if (Player != null)
            StoredPlayerData = Player.GetPlayerStatus();
        else
            StoredPlayerData = new PlayerDataStatus(100, 10); // NOTE Yu Xiang: Forgive me for the magic number, this is Player.MaxHealth and MaxMagic
    }

    /// <summary>
    /// Callback for when a scene has loaded    
    /// </summary>
    /// <param name="scene"></param>
    /// <param name="mode"></param>
    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
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
