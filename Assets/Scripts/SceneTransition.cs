using UnityEngine;
using UnityEngine.UI;

public class SceneTransition : MonoBehaviour
{
    public KeyCode InteractionKey = KeyCode.E;
    public Text InteractionMessage;
    private bool IsPlayerInRange = false;
    private Rigidbody2D PlayerRigidbody;
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
        PlayerScript = playerObj.GetComponent<Player>();
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

        if (PlayerRigidbody != null)
        {
            PlayerRigidbody.gravityScale = 0;
            PlayerRigidbody.velocity = Vector2.zero;
            PlayerRigidbody.AddForce(BlowForce);
        }

        GameManager.Instance.SceneLevelup();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            IsPlayerInRange = true;
            if (InteractionMessage != null)
            {
                InteractionMessage.gameObject.SetActive(true);
                InteractionMessage.text = $"Press '{InteractionKey}' to enter";
            }
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            IsPlayerInRange = false;
            if (InteractionMessage != null)
            {
                InteractionMessage.gameObject.SetActive(false);
            }
        }
    }
}
