using UnityEngine;

public class LevelSelect : MonoBehaviour
{
    [SerializeField] private string level;
    public void LoadSpecificLevel()
    {
        Time.timeScale = 1f;
        GameManager.Instance.LoadSpecificLevel(level, true);
    }
}
