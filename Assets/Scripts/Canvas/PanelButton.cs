// I think this script will integrate into the player manager, but I write it here first by Yu Xiang
using UnityEngine;

public class TimeController : MonoBehaviour {
	public void GotoHomePage() {
		GameManager.Instance.GotoHomePage();
	}

	public void StopTime() {
		MusicManager.Instance.PauseBackgroundMusic();
		Time.timeScale = 0f;
	}

	public void ResumeTime() {
		MusicManager.Instance.PlayOrResumeBackgroundMusic();
		Time.timeScale = 1f;
	}
}
