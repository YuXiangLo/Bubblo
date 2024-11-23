using UnityEngine;

public class MainMenu : MonoBehaviour
{
    public void PlayGame()
    {
        Time.timeScale = 1f;
        GameManager.Instance.StartGame();
    }

    // public void QuitGame()
    // {
    //     // Quit the application (won't work in the editor, but works in a built game)
    //     Application.Quit();
    // }
}
