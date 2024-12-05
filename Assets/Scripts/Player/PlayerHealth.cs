using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    [SerializeField] private float MaxHealth = 100f;
    private float CurrentHealth;
    public float HealthPercentage => CurrentHealth / MaxHealth;
    private Player Player;

    private void Awake()
    {
        Player = GetComponent<Player>();
    }

    public void TakeDamage(float damage)
    {
        CurrentHealth = Mathf.Clamp(CurrentHealth - damage, 0f, MaxHealth);
        if (CurrentHealth == 0)
        {
            Player.Die();
        }
    }

    public void Heal(float amount)
    {
        CurrentHealth = Mathf.Clamp(CurrentHealth + amount, 0f, MaxHealth);
    }

    public void Initialize() => CurrentHealth = MaxHealth;
    public void Initialize(float health) => CurrentHealth = health;
}