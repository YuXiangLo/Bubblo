using UnityEngine;

public class MainMenu : MonoBehaviour
{
    public void PlayGame()
    {
        Time.timeScale = 1f;
        SoundManager.PlaySound(SoundType.Button, (int)ButtonSoundType.Pressed);
        GameManager.Instance.StartGame();
    }
}
