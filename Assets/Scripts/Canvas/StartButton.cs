using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public void PlayGame()
    {
        // Load the main game scene (you must create this scene first and add it to Build Settings)
        SceneManager.LoadScene("Scene1");
    }

    public void QuitGame()
    {
        // Quit the application (won't work in the editor, but works in a built game)
        Application.Quit();
    }
}
