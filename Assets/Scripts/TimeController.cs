// I think this script will integrate into the player manager, but I write it here first by Yu Xiang
using UnityEngine;

public class TimeController : MonoBehaviour {
	public void StopTime() {
		Time.timeScale = 0f;
	}

	public void ResumeTime() {
		Time.timeScale = 1f;
	}
}
