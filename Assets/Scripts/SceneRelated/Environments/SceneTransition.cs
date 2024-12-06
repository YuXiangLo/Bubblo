using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class SceneTransition : MonoBehaviour
{
    [SerializeField] private string NextLevel = ""; // If empty, we run level up, else we go to this level

    private bool IsPlayerInRange = false;
    private float TransitionDelay = 1f;
    private KeyCode InteractionKey = KeyCode.F;

    // Auto Get
    private Player PlayerScript;
	private BoxCollider2D PlayerCollider;
    private MonoBehaviour CameraFollowScript;
    private Rigidbody2D PlayerRigidbody;
    private Vector2 BlowVelocity = new Vector2(15, 9);

    private void Start()
    {
        GameObject playerObj = GameObject.FindWithTag("Player");
		GameObject camera = GameObject.FindWithTag("MainCamera");
        PlayerScript = playerObj.GetComponent<Player>();
		PlayerCollider = playerObj.GetComponent<BoxCollider2D>();
		CameraFollowScript = camera.GetComponent<CameraFollow>();
        PlayerRigidbody = playerObj.GetComponent<Rigidbody2D>();
    }
    private void Update()
    {
        if (IsPlayerInRange && Input.GetKeyDown(InteractionKey))
        {
            BlowAwayPlayer();
        }
    }

	private void BlowAwayPlayer()
	{
		if (PlayerScript != null)
		{
			PlayerScript.enabled = false;
		}

		if (CameraFollowScript)
		{
			CameraFollowScript.enabled = false;
		}
		if (PlayerCollider)
		{
			PlayerCollider.enabled = false;
		}

		if (PlayerRigidbody != null)
		{
			PlayerRigidbody.gravityScale = 0;
			PlayerRigidbody.velocity = BlowVelocity;
		}

     
        StartCoroutine(LoadNextLevel(NextLevel));
	}

	private IEnumerator LoadNextLevel(string nextLevel)
	{
		yield return new WaitForSeconds(TransitionDelay);
        if (nextLevel == "")
            GameManager.Instance.SceneLevelup();
        else
            GameManager.Instance.LoadSpecificLevel(nextLevel, false);
	}

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            IsPlayerInRange = true;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            IsPlayerInRange = false;
        }
    }
}
