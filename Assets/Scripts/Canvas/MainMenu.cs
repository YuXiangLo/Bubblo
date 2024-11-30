using UnityEngine;

public class MainMenu : MonoBehaviour
{
    public void PlayGame()
    {
        Time.timeScale = 1f;
        GameManager.Instance.StartGame();
    }

    public void PlayButtonPushedSoundEffect()
    {
        MusicManager.Instance.PlayButtonPushedSoundEffect();
    }
}
