using UnityEngine;

public class SimpleUnicorn : MonoBehaviour, IModifyHealth
{
    private SimpleUnicornMovement SimpleUnicornMovement;
    private SimpleUnicornHealth SimpleUnicornHealth;

    public void TakeDamage(float amount)
    {
        SimpleUnicornHealth.TakeDamage(amount);
    }

    public void Heal(float amount)
    {
        SimpleUnicornHealth.Heal(amount);
    }

    private void Awake()
    {
        SimpleUnicornMovement = GetComponent<SimpleUnicornMovement>();
        SimpleUnicornHealth = GetComponent<SimpleUnicornHealth>();
    }

    private void Update()
    {
        if (SimpleUnicornMovement.enabled)
            SimpleUnicornMovement.HandleMovement();
    }
}