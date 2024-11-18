using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class SceneTransition : MonoBehaviour
{
    public KeyCode InteractionKey = KeyCode.E;
    public Text InteractionMessage;
    private bool IsPlayerInRange = false;
    private Rigidbody2D PlayerRigidbody;
	private BoxCollider2D PlayerCollider;
    private Player PlayerScript;
    public MonoBehaviour CameraFollowScript;
    public Vector2 BlowForce = new Vector2(500, 300);
    public float TransitionDelay = 1f;

    private void Start()
    {
        if (InteractionMessage != null)
        {
            InteractionMessage.gameObject.SetActive(false);
        }

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
			PlayerRigidbody.velocity = new Vector2(5, 3); // Corrected vector syntax
			PlayerRigidbody.AddForce(BlowForce);
		}

		StartCoroutine(LevelUpAfterDelay());
	}

	private IEnumerator LevelUpAfterDelay()
	{
		yield return new WaitForSeconds(1f); // Wait for 1 second
		GameManager.Instance.SceneLevelup();
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
