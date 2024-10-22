using Unity.VisualScripting;
using UnityEditor.MPE;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }
    public int world { get; private set; }



    private void Awake()
    {
        if(Instance != null){
            DestroyImmediate(gameObject);
        }else{
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }

    private void OnDestroy()
    {
        if(Instance == this){
            Instance = null;
        }
    }

    private void Start()
    {
        LoadLevel(1);
    }

    private void LoadLevel(int world)
    {
        this.world = world;
        SceneManager.LoadScene($"Scene{world}");
    }

    public void NextLevel()
    {
        LoadLevel(world + 1);
    }

    public void ResetLevel(float delay)
    {
        Invoke(nameof(ResetLevel), delay);
    }
    
    public void ResetLevel()
    {
        GameOver();
    }

    public void GameOver()
    {
        NewGame();
    }

    private void NewGame()
    {
        SceneManager.LoadScene("Start");
    }
}
