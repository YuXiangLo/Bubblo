using UnityEngine;
using UnityEngine.SceneManagement;
using System;

public class GameManager : MonoBehaviour
{
    public bool isGameOver = false;
    public static GameManager Instance;
    private Player Player;
    private float CurrentHealthTemp;
    private float CurrentMagicPointTemp;
    [SerializeField] private SceneList SceneList;
    public PlayerHealth PlayerHealth;

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
        SceneManager.LoadScene(SceneList.Scene[1]);
    }

    private void StoreCurrentStates()
    {
        CurrentHealthTemp = Player.CurrentHealth;
        CurrentMagicPointTemp = Player.CurrentMagicPoint;
    }

    /// <summary>
    /// Transition to the next scene
    /// </summary>
    public void SceneLevelup()
    {
        string currentSceneName = SceneManager.GetActiveScene().name;
        int index = Array.IndexOf(SceneList.Scene, currentSceneName);
        StoreCurrentStates();
        string nextSceneName = SceneList.Scene[index + 1];
        SceneManager.sceneLoaded += OnSceneLoaded;
        SceneManager.LoadScene(nextSceneName);
    }

    // private IEnumerator TransitionAfterDelay(string sceneToLoad)
    // {
    //     yield return new WaitForSeconds(TransitionDelay);
    //     SceneManager.LoadScene(sceneToLoad);
    // }

    /// <summary>
    /// Callback for when a scene has loaded    
    /// </summary>
    /// <param name="scene"></param>
    /// <param name="mode"></param>
    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if (scene.name == "Scene1")
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
