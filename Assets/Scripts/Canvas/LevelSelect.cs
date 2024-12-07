using UnityEngine;

public class LevelSelect : MonoBehaviour
{
    [SerializeField] private string level;
    public void LoadSpecificLevel()
    {
        Time.timeScale = 1f;
        SoundManager.PlaySound(SoundType.Button, (int)ButtonSoundType.Pressed);
        GameManager.Instance.LoadSpecificLevel(level, true);
    }
}
