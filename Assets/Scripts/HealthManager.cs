        using UnityEngine;
    using UnityEngine.UI;

    public class HealthManager : MonoBehaviour
    {
        public Image healthBar;
        public float healthAmount = 100f;
        public const float maxHealthAmount = 100f;

        void Update() {
            if (Input.GetKeyDown(KeyCode.Return))
                TakeDamage(20);
            if (Input.GetKeyDown(KeyCode.Space))
                Heal(5);    
        }

        public void TakeDamage(float damage) {
            healthAmount -= damage;
            healthBar.fillAmount = healthAmount / maxHealthAmount;
        }

        public void Heal(float heal) {
            healthAmount += heal;
            healthAmount = Mathf.Clamp(healthAmount, 0, maxHealthAmount);

            healthBar.fillAmount = healthAmount / maxHealthAmount;

        }
    }
