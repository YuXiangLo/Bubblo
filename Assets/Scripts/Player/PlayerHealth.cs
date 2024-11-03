using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;       

public class PlayerHealth : MonoBehaviour
{
    public float MaxHealth = 100f;

	[SerializeField] private string DeadScene = "Start";
	[SerializeField] private float TransitionDelay = 1f;

    private Player Player;

    private void Start()
    {
        Player = GetComponent<Player>();
        // Initialize health at the beginning of the game
        Player.CurrentHealth = MaxHealth;
    }

    public void TakeDamage(float damage)
    {
        // Decrease the player's health
        Player.CurrentHealth -= damage;
        Player.CurrentHealth = Mathf.Clamp(Player.CurrentHealth, 0, MaxHealth);

        if (Player.CurrentHealth <= 0)
        {
            Die();  // Trigger death if health is zero
        }
    }

    public void Heal(float amount)
    {
        // Increase the player's health
        Player.CurrentHealth += amount;
        Player.CurrentHealth = Mathf.Clamp(Player.CurrentHealth, 0, MaxHealth);
    }

    private void Die()
    {
		// 1. Disable PlayerMovement
		Player.enabled = false;
		Time.timeScale = 0f;
		// 2. Play a die animation and load the scene
        StartCoroutine(DelayLoadScene());
    }

	private IEnumerator DelayLoadScene()
	{
		// TODO Add the animation on it, the below code is a placeholder
        yield return new WaitForSecondsRealtime(TransitionDelay);
        SceneManager.LoadScene(DeadScene);
	}

#if UNITY_EDITOR
    private void Update()
    {
        // Debug code to increase and decrease health with key presses
        if (Input.GetKeyDown(KeyCode.H))
        {
            TakeDamage(10f); // Decrease health by 10 when 'H' is pressed
            Debug.Log("Health decreased by 10. Current Health: " + Player.CurrentHealth);
        }

        if (Input.GetKeyDown(KeyCode.J))
        {
            Heal(10f); // Increase health by 10 when 'J' is pressed
            Debug.Log("Health increased by 10. Current Health: " + Player.CurrentHealth);
        }
    }
#endif
}

